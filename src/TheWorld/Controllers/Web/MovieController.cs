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

        public IActionResult Index(int page = 1)
        {
            
            return View(new MoviesViewModel() {Movies = _movieRepository.GetMoviesOnPage(page), PageNumber = page, PageSize = 20, TotalItemCount = _movieRepository.GetTotalMovies()});
        }

        public IActionResult Movie(int id)
        {
            var result = _movieRepository.GetById(id);
            return View(result);
        }
    }
}
