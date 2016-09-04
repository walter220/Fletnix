using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using TheWorld.Models;
using TheWorld.Models.Repositories;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly FletnixContext _context;
        private readonly ILogger<MovieRepository> _logger;
        private readonly UserManager<FletnixUser> _manager;

        public UserController(FletnixContext context, ILogger<MovieRepository> logger, UserManager<FletnixUser> manager)
        {
            _context = context;
            _logger = logger;
            _manager = manager;
        }

        [Authorize]
        public async Task<ActionResult> Index()
        {
            ViewBag.user = await _manager.FindByNameAsync(User.Identity.Name);
            string mail = ViewBag.user.Email;

            Customer user;

            try
            {
                user = _context.Customer
                    .Include(c => c.Watchhistory)
                    .ThenInclude(w => w.movie)
                    .FirstOrDefault(c => c.customer_mail_address == mail);
                //ToDo: FirstOrDefault must be replaced by first after proper db fix
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get stuff from DB", ex);
                return null;
            }

            return View(user);
        }
    }
}