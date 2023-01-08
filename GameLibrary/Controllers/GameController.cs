using GameLibrary.Areas.Admin.Constants;
using GameLibrary.Core.Contracts;
using GameLibrary.Core.Models;
using GameLibrary.Core.Models.Game;
using GameLibrary.Extensions;
using GameLibrary.Infrastructure.Data.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static GameLibrary.Areas.Admin.Constants.AdminConstants;
using static GameLibrary.Infrastructure.Data.Constants.GameDeveloperConstants;
using GameLibrary.Core.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using static GameLibrary.Areas.Admin.Constants.UserConstants;
using GameLibrary.Core.Models.Admin;

namespace GameLibrary.Controllers
{
    [AuthorizeRoles(AdminRole, UserRole, GameDeveloperRole)]
    public class GameController : Controller
    {
        private readonly IGameService gameService;

        private readonly ILogger logger;

        //private readonly IDistributedCache cache;

        public GameController(
            IGameService gameService,
            ILogger<GameController> logger
            /*IDistributedCache cache*/)
        {
            this.gameService = gameService;
            this.logger = logger;
            //this.cache = cache;
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
                logger.LogInformation("Add Model validation error seems to have occured. {0}", model);
                TempData[MessageConstant.WarningMessage] = "Requirements do not apply";
                return View(model);
            }

            try
            {
                await gameService.AddGameAsync(model, User.Id());
                TempData[MessageConstant.SuccessMessage] = "Success!";
                return RedirectToAction(nameof(All));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "AddGame Savechanges was not executed");
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
            catch (Exception ex)
            {
                logger.LogError(ex, "Marked a game ,with {0} gameId, as favoured went wrong", gameId);
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
            catch (Exception ex)
            {
                logger.LogError(ex, "Marked a game ,with {0} gameId, as unfavoured went wrong with this userId {1}",
                    gameId, this.User.Id());
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
        public async Task<IActionResult> Details(int gameId, string information)
        {
            if (!await gameService.CheckIfGameExistsById(gameId))
            {
                logger.LogError("Game cant be acquired thru gameId at DetailsPage.");
                TempData[MessageConstant.ErrorMessage] = "Game not found";
                return RedirectToAction(nameof(All));
            }

            try
            {
                var model = await gameService.ShowDetailsPage(gameId);

                var informationModel = await gameService.GetGameById(gameId);

                if (information != informationModel.GetInformation())
                {
                    logger.LogInformation("User {0} with UserId is Parameter tampering.", this.User.Id());
                    TempData[MessageConstant.ErrorMessage] = "Wrong Info";
                    return RedirectToAction(nameof(All));
                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Game Id {0} was not found or not taken correctly at the detailsPage", gameId);
                TempData[MessageConstant.ErrorMessage] = "Invalid!";

                return RedirectToAction(nameof(All));
            }

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
                logger.LogInformation("Edit model validation went wrong. {0}", model);
                TempData[MessageConstant.WarningMessage] = "Requirements do not apply";
                return View(model);
            }

            try
            {
                await gameService.EditGameAsync(model);
                TempData[MessageConstant.SuccessMessage] = "Success!";
                return RedirectToAction(nameof(All));
            }
            catch (Exception ex)
            {
                model.Themes = await gameService.GetAllThemes();
                model.Genres = await gameService.GetAllGenres();

                logger.LogError(ex, "Savechanges at Editpost turned problematic");

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
                logger.LogInformation("Deleting a post with {0} gameId could not be found", gameId);
                TempData[MessageConstant.WarningMessage] = "Request not understood. Reload page.";
                return View(model);
            }

            try
            {
                await gameService.DeleteGamePost(gameId);
                TempData[MessageConstant.SuccessMessage] = "Successfully deleted!";
                return RedirectToAction(nameof(All));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured when deleting a game with {0} gameId", gameId);
                TempData[MessageConstant.ErrorMessage] = "Unsuccessful!";
                return View(model);
            }
        }

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
            if (!await gameService.CheckIfGameExistsById(gameId))
            {
                logger.LogInformation("Commenting a post with {0} gameId could not be found", gameId);
                TempData[MessageConstant.WarningMessage] = "Request not understood. Reload page.";
                return RedirectToAction(nameof(All));
            }

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
            catch (Exception ex)
            {
                logger.LogError(ex, "Comment with {0} model was not added or not taken correctly with the {1} gameId",
                    model, gameId);
                TempData[MessageConstant.ErrorMessage] = "Invalid!";

                return View(model);
            }

            try
            {
                var newModel = await gameService.GetGameById(gameId);

                return RedirectToAction(nameof(Details), new { gameId = gameId, information = newModel.GetInformation() });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Post is null with Id {0} at details page.", gameId);
                TempData[MessageConstant.ErrorMessage] = "Invalid!";
            }

            return RedirectToAction(nameof(All));
        }

        /// <summary>
        /// Likes a post
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns>Savechanges</returns>

        [HttpPost]
        public async Task<IActionResult> Like(int gameId)
        {
            if (!await gameService.CheckIfGameExistsById(gameId))
            {
                logger.LogInformation("Liking a post with {0} gameId could not be found", gameId);
                TempData[MessageConstant.WarningMessage] = "Request not understood. Reload page.";
                return View(nameof(All));
            }

            try
            {
                await gameService.LikePost(gameId, this.User.Id());
                
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Liking a post with {0} gameId turned incorrect", gameId);
                TempData[MessageConstant.ErrorMessage] = "Invalid!";
            }

            try
            {
                var model = await gameService.GetGameById(gameId);
                return RedirectToAction(nameof(Details), new { gameId = gameId, information = model.GetInformation() });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Post is null with Id {0} at details page.", gameId);
                TempData[MessageConstant.ErrorMessage] = "Invalid!";
            }

            //If it goes here, then something went wrong.
            return RedirectToAction(nameof(All));
        }

        /// <summary>
        /// Dislikes gamepost
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns>Savechanges</returns>
        [HttpPost]
        public async Task<IActionResult> Dislike(int gameId)
        {
            if (!await gameService.CheckIfGameExistsById(gameId))
            {
                logger.LogInformation("Disliking a post with {0} gameId could not be found", gameId);
                TempData[MessageConstant.WarningMessage] = "Request not understood. Reload page.";
                return View(nameof(All));
            }

            try
            {
                await gameService.DislikePost(gameId, this.User.Id());

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Dislking a post with {0} gameId turned incorrect", gameId);
                TempData[MessageConstant.ErrorMessage] = "Invalid!";
            }

            try
            {
                var model = await gameService.GetGameById(gameId);
                return RedirectToAction(nameof(Details), new { gameId = gameId, information = model.GetInformation() });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Post is null with Id {0} at details page.", gameId);
                TempData[MessageConstant.ErrorMessage] = "Invalid!";
            }

            //If it goes here, then something went wrong.
            return RedirectToAction(nameof(All));
        }

        /// <summary>
        /// Removes a desired comment by Id
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RemoveComment(int commentId, int gameId)
        {
            if (!await gameService.CheckIfGameExistsById(gameId))
            {
                logger.LogInformation("Removing a comment with commentId {0} on post with {1} gameId could not be found", 
                    commentId ,gameId);
                TempData[MessageConstant.WarningMessage] = "Request not understood. Reload page.";
                return View(nameof(All));
            }

            try
            {
                await gameService.RemoveGameCommentById(commentId, gameId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Removing a comment with commentId {0} on post with {1} gameId could" +
                    " not be found", gameId, commentId);
                TempData[MessageConstant.ErrorMessage] = "Invalid!";
            }

            var model = await gameService.GetGameById(gameId);

            return RedirectToAction(nameof(Details), new { gameId = gameId, information = model.GetInformation() });
        }


        public async Task<IActionResult> DeveloperGame()
        {
            if (!this.User.IsInRole(GameDeveloperRole))
            {
                TempData[MessageConstant.WarningMessage] = "You are not a Game Developer!";
                return RedirectToAction("All", "Game");
            }

            try
            {
                var model = await gameService.CreatorsLibrary(this.User.Id());
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return RedirectToAction(nameof(All));
            }
        }
        
    }
}

