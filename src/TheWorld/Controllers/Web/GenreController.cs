using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;
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

        public IActionResult Genre(string id)
        {
            return View(_repository.GetByGenreName(id));
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Genre g)
        {
            _repository.CreateGenre(g);
            return RedirectToAction("Index", "Genre");
        }

        [Authorize]
        public IActionResult Edit(string genre)
        {
            return View(_repository.GetAll());
        }

        [HttpPut]
        [Authorize]
        public IActionResult Edit()
        {
            return View(_repository.GetAll());
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            _repository.DeleteGenre(id);
            return RedirectToAction("Index", "Genre");
        }
        
        public IActionResult Delete()
        {
            return RedirectToAction("Index", "Genre");
        }
    }
}