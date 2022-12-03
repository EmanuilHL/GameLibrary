using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
