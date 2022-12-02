using GameLibrary.Core.Contracts;
using GameLibrary.Core.Models;
using GameLibrary.Core.Models.Game;
using GameLibrary.Extensions;
using GameLibrary.Infrastructure.Data.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GameLibrary.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        /// <summary>
        /// Idea: Its YOUR gamelibrary. es macht mehr sinn. Since you add games you played or just add games for the future
        /// and add favourites
        /// </summary>
        private readonly IGameService gameService;

        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        /// <summary>
        /// Add View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new GameFormViewModel()
            {
                Genres = await gameService.GetAllGenres(),
                Themes = await gameService.GetAllThemes(),
                UserId = this.User.Id()
            };

            return View(model);
        }

        /// <summary>
        /// Adds a post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(GameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Themes = await gameService.GetAllThemes();
                model.Genres = await gameService.GetAllGenres();
                TempData[MessageConstant.WarningMessage] = "Requirements do not apply";
                return View(model);
            }

            try
            {
                await gameService.AddGameAsync(model, User.Id());
                TempData[MessageConstant.SuccessMessage] = "Success!";
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                TempData[MessageConstant.ErrorMessage] = "Invalid!";

                return View(model);
            }
        }

        /// <summary>
        /// Marks a post as favourite and adds it to a personalized Collection.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> MarkAsFavourite(int gameId)
        {
            try
            {
                await gameService.MarkGameAsFavourite(this.User.Id(), gameId);
                TempData[MessageConstant.SuccessMessage] = "Successfully favourited the game!";
                return RedirectToAction(nameof(Favourites));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                TempData[MessageConstant.ErrorMessage] = "Invalid!";

                return View(nameof(All));
            }
        }
        /// <summary>
        /// Shows all the favourited gameposts by the user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Favourites()
        {
            var models = await gameService.ShowAllFavourites(this.User.Id());

            return View(models);
        }

        [HttpPost]
        public async Task<IActionResult> UnFavourite(int gameId)
        {
            try
            {
                await gameService.MarkGameAsUnfavourite(gameId, this.User.Id());
                TempData[MessageConstant.SuccessMessage] = "Successfully unfavourited the game!";
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                TempData[MessageConstant.ErrorMessage] = "Invalid!";

                return View(nameof(Favourites));
            }
        }

        /// <summary>
        /// Gets the details of a gamepost.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int gameId)
        {
            //ToDO: Comments and new Idea maybe likes.

            try
            {
                var model = await gameService.ShowDetailsPage(gameId);


                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                TempData[MessageConstant.ErrorMessage] = "Invalid!";
            }

            return Ok();
        }

        /// <summary>
        /// Edits a post (Only accessible by the owner of the post)
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int gameId)
        {
             var model = await gameService.FindEditPostById(gameId);

            return View(model);
        }


        /// <summary>
        /// Edits a post (Only accessible by the owner of the post)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(GameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Themes = await gameService.GetAllThemes();
                model.Genres = await gameService.GetAllGenres();
                TempData[MessageConstant.WarningMessage] = "Requirements do not apply";
                return View(model);
            }

            try
            {
                await gameService.EditGameAsync(model);
                TempData[MessageConstant.SuccessMessage] = "Success!";
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                model.Themes = await gameService.GetAllThemes();
                model.Genres = await gameService.GetAllGenres();

                ModelState.AddModelError("", "Something went wrong");
                TempData[MessageConstant.ErrorMessage] = "Invalid!";

                return View(model);
            }
        }

        /// <summary>
        /// Shows the soon to be possible deleted view.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int gameId)
        {
            var model = await gameService.GetGameToDelete(gameId);

            return View(model);
        }

        /// <summary>
        /// Deletes a gamepost
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(int gameId, GameViewModel? model = null)
        {

            if (!await gameService.CheckIfGameExistsById(gameId))
            {
                return BadRequest();
            }

            try
            {
                await gameService.DeleteGamePost(gameId);
                TempData[MessageConstant.SuccessMessage] = "Successfully deleted!";
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                TempData[MessageConstant.ErrorMessage] = "Unsuccessful!";
                return View(model);
            }
        }


        //[HttpGet]
        //public async Task<IActionResult> Search()
        //{
        //    var model = new GameViewModel()
        //    {
        //        Themes = await gameService.GetAllThemes()
        //    };

        //    return View(model);
        //}

        /// <summary>
        /// Searches for a game.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] SearchQueryViewModel query)
        {
            var result = await gameService.Search(query.Theme, query.SearchTerm, query.Sorting);


            query.Themes = await gameService.GetAllThemes();
            query.Games = result.Games;

            return View(query);
        }

        /// <summary>
        /// This is only for the COmment FORUM. All the commets should be shown on the details page.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Comment(int gameId)
        { 
            var model = await gameService.GetCommentView(gameId, this.User.Id());

            return View(model);
        }

        /// <summary>
        /// Creates a comment
        /// </summary>
        /// <param name="model"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Comment(CommentPostModel model, int gameId)
        {
            try
            {
                await gameService.AddComment(model, gameId, this.User.Id());
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction(nameof(Details), new { gameId }); 
        }



        /// <summary>
        /// Likes a post
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns>Savechanges</returns>

        [HttpPost]
        public async Task<IActionResult> Like(int gameId)
        {
            try
            {
                await gameService.LikePost(gameId, this.User.Id());
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction(nameof(Details), new { gameId });
        }

        /// <summary>
        /// Dislikes gamepost
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns>Savechanges</returns>
        [HttpPost]
        public async Task<IActionResult> Dislike(int gameId)
        {
            try
            {
                await gameService.DislikePost(gameId, this.User.Id());
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction(nameof(Details), new { gameId });
        }

        //Tempdata from toastr doesnt work.
        //public async Task<IActionResult> Responses()
        //{

        //}
    }
}

