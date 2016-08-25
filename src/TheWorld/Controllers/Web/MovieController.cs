using System;
using System.Linq;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;
using TheWorld.Models.Repositories;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    [Authorize]
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository repository)
        {
            _movieRepository = repository;
        }

        
        public IActionResult Index(int page = 1, string search = "", int size = 10)
        {
            var movies = _movieRepository.GetAllMovies(page, search, size);
            var movieView = new MoviesViewModel()
            {
                Movies = movies,
                Search = search,
                PageNumber = page,
                PageSize = size,
                TotalItemCount = _movieRepository.GetTotalMovies(search)
            };
            return View(movieView);
        }
        
        public IActionResult Movie(int id)
        {
            return View(_movieRepository.GetMovie(id));
        }
        
        public IActionResult Watch(int id)
        {
            return View(_movieRepository.GetMovie(id));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new Movie() { movie_id = _movieRepository.GetNewMovieId() });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie m)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Er is een fout opgetreden");
                return View(m);
            }
            _movieRepository.CreateMovie(m);
            return RedirectToAction("Movie", "Movie", m.movie_id);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            return View(_movieRepository.GetMovie(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Movie m)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Er is een fout opgetreden");
                return View(m);
            }
            _movieRepository.UpdateMovie(m);
            return RedirectToAction("Movie", "Movie", m.movie_id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _movieRepository.DeleteMovie(id);
            return RedirectToAction("Index", "Movie");
        }
    }
}
