using GameLibrary.Areas.Admin.Constants;
using GameLibrary.Core.Models.User;
using GameLibrary.Infrastructure.Data.Constants;
using GameLibrary.Infrastructure.Data.Entities;
using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static GameLibrary.Areas.Admin.Constants.UserConstants;

namespace GameLibrary.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;

        private readonly SignInManager<User> signInManager;

        private readonly RoleManager<IdentityRole> roleManager;

        private readonly ILogger logger;

        public UserController(
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            RoleManager<IdentityRole> _roleManager,
            ILogger<UserController> _logger)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
            logger = _logger;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            HtmlSanitizer sanitizer = new HtmlSanitizer();
            if (!ModelState.IsValid)
            {
                logger.LogInformation("Register Modelvalidation error");
                TempData[MessageConstant.WarningMessage] = "Requirements do not apply!";
                return View(model);
            }

            var user = new User()
            {
                Email = sanitizer.Sanitize(model.Email),
                UserName = sanitizer.Sanitize(model.UserName)
            };

            var result = await userManager.CreateAsync(user, sanitizer.Sanitize(model.Password));

            if (!await roleManager.RoleExistsAsync(UserRole))
            {
                var newRole = new IdentityRole { Name = UserRole };
                await roleManager.CreateAsync(newRole);
            }

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, UserRole);
                return RedirectToAction("Login", "User");
            }
            TempData[MessageConstant.ErrorMessage] = "Could not register";
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            HtmlSanitizer sanitizer = new HtmlSanitizer();
            if (!ModelState.IsValid)
            {
                logger.LogInformation("Login Modelvalidation error");
                TempData[MessageConstant.WarningMessage] = "Requirements do not apply!";
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, sanitizer.Sanitize(model.Password), false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid login");
            TempData[MessageConstant.ErrorMessage] = "Error occured!";

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            TempData[MessageConstant.SuccessMessage] = "Successfully logged out!";

            return RedirectToAction("Index", "Home");
        }
    }
}
