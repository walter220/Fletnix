using System.Linq;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;
using TheWorld.Models.Repositories;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genreRepository;

        public AppController(IMovieRepository repository, IGenreRepository genreRepository)
        {
            _movieRepository = repository;
            _genreRepository = genreRepository;
        }

        public IActionResult Index()
        {
            return View(_movieRepository.GetPopularMovies());
        }
        
        [Authorize]
        public IActionResult About()
        {
            return View();
        }
        
        public IActionResult Contact()
        {
            return View();
        }

//        [HttpPost]
//        public IActionResult Contact(ContactViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var email = Startup.Configuration["AppSettings:SiteEmailAddress"];
//
//                if (string.IsNullOrWhiteSpace(email))
//                {
//                    ModelState.AddModelError("", "Could not send email, configuration problem");
//                }
//
//                if (_mailService.SendMail(email, email,
//                    $"Contact Page from {model.Name} ({model.Email})",
//                    model.Message))
//                {
//                    ModelState.Clear();
//                    ViewBag.Message = "Mail Send!";
//                }
//            }
//            return View();
//        }
    }
}
