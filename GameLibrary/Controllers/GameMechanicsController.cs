using GameLibrary.Core.Contracts;
using GameLibrary.Core.Models.GameMechanic;
using GameLibrary.Extensions;
using GameLibrary.Infrastructure.Data.Constants;
using GameLibrary.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static GameLibrary.Areas.Admin.Constants.UserConstants;
using static GameLibrary.Areas.Admin.Constants.AdminConstants;

namespace GameLibrary.Controllers
{
    [AuthorizeRoles(UserRole, AdminRole)]
    public class GameMechanicsController : Controller
    {
        private readonly ICareerService helperService;

        private readonly IGameMechanicService mechanicsService;

        private readonly IGameService gameService;

        private readonly ILogger logger;

        public GameMechanicsController(
            ICareerService helperService,
            IGameMechanicService mechanicsService,
            IGameService gameService,
            ILogger<GameMechanicsController> logger)
        {
            this.helperService = helperService;
            this.mechanicsService = mechanicsService;
            this.gameService = gameService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Send()
        {
            if (!await helperService.ExistsById(this.User.Id()))
            {
                logger.LogInformation("An unclassified User {0} is attempting to send a gamepost developer a message", this.User.Id());
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
            if (!ModelState.IsValid)
            {
                logger.LogInformation("An unclassified User {0} is attempting to send a gamepost developer a message", this.User.Id());
                ModelState.AddModelError("", "Validation at GameMechanicsController at Send went wrong.");
                return View(model);

            }

            try
            {
                await mechanicsService.CreateGameMechanic(model, this.User.Id());
                TempData[MessageConstant.SuccessMessage] = "Successfully sent!";
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex, "A send error occured when sending a mechanic report as helper");
                TempData[MessageConstant.ErrorMessage] = "Unsuccessful!";
                return View(model);
            }

            return RedirectToAction("All", "Game");
        }

        [HttpGet]
        public async Task<IActionResult> Service()
        {
            if (!await gameService.IsUserDevelepor(this.User.Id()))
            {
                TempData[MessageConstant.WarningMessage] = "You are not a GamePost Developer!";
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
            catch (Exception ex)
            {
                logger.LogInformation(ex, "An Error occured when deleting a GameMechanic Post with {0} mechanicId", mechanicId);
                TempData[MessageConstant.ErrorMessage] = "Invalid Attempt";
                return RedirectToAction(nameof(Service));
            }
            
            return RedirectToAction(nameof(Service));
        }
    }
}
