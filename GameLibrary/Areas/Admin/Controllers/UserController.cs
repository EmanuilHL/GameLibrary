using GameLibrary.Core.Contracts;
using GameLibrary.Core.Contracts.Admin;
using GameLibrary.Infrastructure.Data.Constants;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;
        private readonly ICareerService helperService;

        public UserController(
            IUserService _userService,
            ICareerService _helperService)
        {
            userService = _userService;
            helperService = _helperService;
        }

        public async Task<IActionResult> AllUsers()
        {
            var model = await userService.AllUsers();

            return View(model);
        }

        public async Task<IActionResult> AllHelpers()
        {
            var model = await helperService.AllHelpers();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Forget(string userId)
        {
            bool result = await userService.Forget(userId);

            if (result)
            {
                TempData[MessageConstant.SuccessMessage] = "User is now forgotten";
            }
            else
            {
                TempData[MessageConstant.ErrorMessage] = "User is unforgetable";
            }

            return RedirectToAction(nameof(AllUsers));
        }
    }
}
