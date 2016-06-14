using System;
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

        public AppController(IMovieRepository repository)
        {
            _movieRepository = repository;
        }

        public IActionResult Index()
        {
//            return View(_movieRepository.GetPopularMovies());
            return View();
        }
        
        public IActionResult About()
        {
            ViewBag.totalMovies = _movieRepository.GetTotalMovies();
            return View();
        }
        
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Test()
        {
            throw new Exception();
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
