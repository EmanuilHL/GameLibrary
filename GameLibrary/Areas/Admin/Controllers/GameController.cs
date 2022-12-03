using GameLibrary.Areas.Admin.Models;
using GameLibrary.Core.Contracts;
using GameLibrary.Core.Models;
using GameLibrary.Core.Models.Game;
using GameLibrary.Extensions;
using GameLibrary.Infrastructure.Data.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GameLibrary.Areas.Admin.Controllers
{
    public class GameController : BaseController
    {
        private readonly IGameService gameService;

        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public async Task<IActionResult> Mine()
        {
            var model = new AdminGamesViewModel();

            model.GamesAddedByAdmin = await gameService.GetGamesCreatedByUserId(this.User.Id());

            return View(model);
        }
    }
}

