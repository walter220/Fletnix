using Microsoft.AspNet.Mvc;
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

        public IActionResult Index(int page = 1, string search = "", int size = 20)
        {
            
            return View(new MoviesViewModel() {Movies = _movieRepository.GetMoviesOnPage(page, search, size), Search = search, PageNumber = page, PageSize = size, TotalItemCount = _movieRepository.GetTotalMovies()});
        }

        public IActionResult Movie(int id)
        {
            var result = _movieRepository.GetById(id);
            return View(result);
        }
    }
}
