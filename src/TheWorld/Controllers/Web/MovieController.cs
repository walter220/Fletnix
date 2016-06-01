using Microsoft.AspNet.Mvc;
using TheWorld.Models.Repositories;

namespace TheWorld.Controllers.Web
{
    public class MovieController : Controller
    {
        //
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genreRepository;

        public MovieController(IMovieRepository repository, IGenreRepository genreRepository)
        {
            _movieRepository = repository;
            _genreRepository = genreRepository;
        }

        public IActionResult Index()
        {
            return View(_movieRepository.GetAll());
        }

        public IActionResult Movie(int id)
        {
            var result = _movieRepository.GetById(id);
            return View(result);
        }
    }
}
