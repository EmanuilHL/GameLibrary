using GameLibrary.Core.Contracts;
using GameLibrary.Core.Models.GameMechanic;
using GameLibrary.Extensions;
using GameLibrary.Infrastructure.Data.Constants;
using GameLibrary.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Areas.Admin.Controllers
{
    public class GameMechanicsController : BaseController
    {

        private readonly IGameMechanicService mechanicsService;

        public GameMechanicsController(IGameMechanicService mechanicsService)
        {
            this.mechanicsService = mechanicsService;
        }

        public async Task<IActionResult> All()
        {
            var model = await mechanicsService.Reports();

            return View(model);
        }
    }
}
