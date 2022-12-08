using GameLibrary.Core.Contracts;
using GameLibrary.Core.Models.Career;
using GameLibrary.Extensions;
using GameLibrary.Infrastructure.Data.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static GameLibrary.Areas.Admin.Constants.UserConstants;

namespace GameLibrary.Controllers
{
    [AuthorizeRoles(UserRole)]
    public class CareerController : Controller
    {
        private readonly ICareerService careerService;

        private readonly ILogger logger;

        public CareerController(
            ICareerService careerService,
            ILogger<CareerController> logger)
        {
            this.careerService = careerService;
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            if (await careerService.ExistsById(this.User.Id()))
            {
                TempData[MessageConstant.WarningMessage] = "You are already a Helper";

                return RedirectToAction("Index", "Home");
            }

            var model = new BecomeHelperModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(BecomeHelperModel model)
        {
            var userId = User.Id();

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something faulty occured");
                logger.LogInformation("Model Validation at Career / Index went wrong");
                return View(model);
            }

            if (await careerService.HelperWithPhoneNumberExists(model.PhoneNumber))
            {
                TempData[MessageConstant.ErrorMessage] = "Phonenumber already exists.";

                return RedirectToAction(nameof(Index));
            }

            try
            {
                await careerService.CreateHelper(userId, model.PhoneNumber);
                TempData[MessageConstant.SuccessMessage] = "Successfully became a Helper!";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Helper could not be created");
                TempData[MessageConstant.ErrorMessage] = "Unsuccessful!";
            }

            return RedirectToAction("All", "Game");
        }
    }
}
