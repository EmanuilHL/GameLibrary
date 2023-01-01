using GameLibrary.Areas.Admin.Models;
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
        private readonly ILogger<UserController> logger;

        public UserController(
            IUserService _userService,
            ICareerService _helperService,
            ILogger<UserController> _logger)
        {
            userService = _userService;
            helperService = _helperService;
            logger = _logger;
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

        public IActionResult Rank()
        {
            var model = new RankFormModel();

            return View(model);
        }


        //Test RANK function

        /// <summary>
        /// Applies rank to a developer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IActionResult> Rank(RankFormModel model)
        {
            try
            {
                await userService.ApplyRoleToDeveloper(model.UserName);
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                var newModel = new RankFormModel();

                return View(newModel);
            }
        }
    }
}
