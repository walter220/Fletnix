using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;
using TheWorld.Models.Repositories;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class MovieController : Controller
    {
        //
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository repository)
        {
            _movieRepository = repository;
        }

        public IActionResult Index(int page = 1, string search = "", int size = 10)
        {
            
            return View(new MoviesViewModel() {Movies = _movieRepository.GetAllMovies(page, search, size), Search = search, PageNumber = page, PageSize = size, TotalItemCount = _movieRepository.GetTotalMovies()});
        }

        public IActionResult Movie(int id)
        {
            return View(_movieRepository.GetMovie(id));
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id)
        {
            return View(_movieRepository.GetMovie(id));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            return View(_movieRepository.GetMovie(id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Movie m)
        {
            _movieRepository.UpdateMovie(m);
            return RedirectToAction("Movie", "Movie", new { id = m.movie_id});
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            int mId = id;
            return RedirectToAction("Movie", "Movie", new { id = mId});
        }
    }
}
