using GameLibrary.Core.Contracts;
using GameLibrary.Core.Models.Career;
using GameLibrary.Extensions;
using GameLibrary.Infrastructure.Data.Constants;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Controllers
{
    public class CareerController : Controller
    {
        private readonly ICareerService careerService;

        public CareerController(ICareerService careerService)
        {
            this.careerService = careerService;
        }

        public async Task<IActionResult> Index()
        {
            if (await careerService.ExistsById(this.User.Id()))
            {
                TempData[MessageConstant.ErrorMessage] = "You are already a Helper";

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
                return View(model);
            }

            if (await careerService.ExistsById(userId))
            {
                TempData[MessageConstant.ErrorMessage] = "You are already a Helper";

                return RedirectToAction("Index", "Home");
            }

            if (await careerService.UserWithPhoneNumberExists(model.PhoneNumber))
            {
                TempData[MessageConstant.ErrorMessage] = "Phonenumber already exists.";

                return RedirectToAction("Index", "Home");
            }

            await careerService.CreateHelper(userId, model.PhoneNumber);

            return RedirectToAction("All", "Game");
        }
    }
}
