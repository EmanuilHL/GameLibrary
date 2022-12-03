using GameLibrary.Core.Contracts;
using GameLibrary.Core.Models.GameMechanic;
using GameLibrary.Extensions;
using GameLibrary.Infrastructure.Data.Constants;
using GameLibrary.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Controllers
{
    [Authorize]
    public class GameMechanicsController : Controller
    {
        private readonly ICareerService helperService;

        private readonly IGameMechanicService mechanicsService;

        private readonly IGameService gameService;

        public GameMechanicsController(
            ICareerService helperService,
            IGameMechanicService mechanicsService,
            IGameService gameService)
        {
            this.helperService = helperService;
            this.mechanicsService = mechanicsService;
            this.gameService = gameService;
        }

        public async Task<IActionResult> Send()
        {
            if (!await helperService.ExistsById(this.User.Id()))
            {
                TempData[MessageConstant.ErrorMessage] = "You are not a helper";
                return RedirectToAction("Index", "Home");
            }

            var model = new MechanicsFormModel()
            {
                UserId = this.User.Id()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Send(MechanicsFormModel model)
        {
            if (!await helperService.ExistsById(this.User.Id()))
            {
                TempData[MessageConstant.ErrorMessage] = "You are not a helper";
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await mechanicsService.CreateGameMechanic(model, this.User.Id());

            return RedirectToAction("All", "Game");
        }

        [HttpGet]
        public async Task<IActionResult> Service()
        {
            if (!await gameService.IsUserDevelepor(this.User.Id()))
            {
                return RedirectToAction("All", "Game");
            }

            var models = await mechanicsService.All(this.User.Id());

            return View(models);
        }

        [HttpPost]
        public async Task<IActionResult> Confirmation(int mechanicId)
        {

            try
            {
                await mechanicsService.RemoveMechanicReport(mechanicId);
            }
            catch (Exception)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid Attempt";
            }
            

            return RedirectToAction(nameof(Service));
        }
    }
}
