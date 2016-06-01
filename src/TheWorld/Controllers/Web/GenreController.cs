using Microsoft.AspNet.Mvc;
using TheWorld.Models.Repositories;

namespace TheWorld.Controllers.Web
{
    public class GenreController : Controller
    {
        private IGenreRepository _repository;

        public GenreController(IGenreRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(_repository.GetAll());
        }
    }
}