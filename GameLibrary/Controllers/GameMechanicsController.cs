using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Controllers
{
    public class GameMechanicsController : Controller
    {
        public IActionResult Send()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Send()
        //{
        //    return View();
        //}
    }
}
