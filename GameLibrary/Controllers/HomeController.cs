using GameLibrary.Core.Contracts;
using GameLibrary.Infrastructure.Data.Constants;
using GameLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static GameLibrary.Areas.Admin.Constants.AdminConstants;

namespace GameLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger logger;

        private readonly IGameService gameService;

        public HomeController(
            ILogger<HomeController> logger,
            IGameService gameService)
        {
            this.logger = logger;
            this.gameService = gameService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole(AdminRole))
            {
                return RedirectToAction("Index", "Admin", new { area = AreaName });
            }

            try
            {
                var model = await gameService.FindHottestGame();

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An Error occured when finding the hottest game");
                TempData[MessageConstant.ErrorMessage] = "Invalid Attempt";
                return RedirectToAction(nameof(Privacy));
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}