using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace TheWorld.Controllers.Web
{
    [Authorize]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}