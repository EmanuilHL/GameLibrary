using GameLibrary.Core.Contracts;
using GameLibrary.Core.Contracts.Admin;
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
    }
}
