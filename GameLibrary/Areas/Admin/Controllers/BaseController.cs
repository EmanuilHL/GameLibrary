using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static GameLibrary.Areas.Admin.Constants.AdminConstants;

namespace GameLibrary.Areas.Admin.Controllers
{
    [Area(AreaName)]
    [Route("Admin/[controller]/[Action]/{id?}")]
    [Authorize(Roles = AdminRole)]
    public class BaseController : Controller
    {
    }
}
