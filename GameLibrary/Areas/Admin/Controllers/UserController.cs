using GameLibrary.Core.Contracts.Admin;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        public async Task<IActionResult> AllUsers()
        {
            var model = await userService.AllUsers();

            return View(model);
        }

        public async Task<IActionResult> AllHelpers()
        {
            var model = await userService.AllHelpers();

            return View(model);
        }
    }
}
