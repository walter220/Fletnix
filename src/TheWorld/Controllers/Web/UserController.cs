using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;

namespace TheWorld.Controllers.Web
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly FletnixContext _context;

        public UserController(FletnixContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Customer")]
        public IActionResult Index()
        {
            return View();
        }
    }
}