using GameLibrary.Core.Contracts;
using GameLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IGameService gameService;

        public HomeController(
            ILogger<HomeController> logger,
            IGameService gameService)
        {
            _logger = logger;
            this.gameService = gameService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await gameService.FindHottestGame();

                return View(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
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