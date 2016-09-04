using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
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
        private readonly IGenreRepository _genreRepository;
        private UserManager<FletnixUser> _manager;

        public MovieController(IMovieRepository movie, IGenreRepository genre, UserManager<FletnixUser> manager)
        {
            _movieRepository = movie;
            _genreRepository = genre;
            _manager = manager;
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
        
        public async Task<IActionResult> Movie(int id)
        {
            Movie m = _movieRepository.GetMovie(id);
            ViewBag.user = await _manager.FindByNameAsync(User.Identity.Name);
            m.Watchhistory = m.Watchhistory
                .Where(w => w.customer_mail_address == ViewBag.user.Email &&
                            w.movie_id == m.movie_id).ToList();
            return View(m);
        }

        public async Task<ActionResult> Watch(int id)
        {
            Movie m = _movieRepository.GetMovie(id);
            var user = await _manager.FindByNameAsync(User.Identity.Name);
            string mail = user.Email;

            if (m != null && mail != null)
            {
                Watchhistory w = new Watchhistory();
                w.movie_id = m.movie_id;
                w.customer_mail_address = mail;
                w.price = m.price;
                w.watch_date = DateTime.Now.Date;
                w.invoiced = false;

                try
                {
                    _movieRepository.WatchMovie(w);
                }
                catch
                {
                    return RedirectToAction("Movie", "Movie", new { id = m.movie_id });
                }
            }
            return View(m);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new MovieViewModel()
            {
                Movie = new Movie() {movie_id = _movieRepository.GetNewMovieId()},
                Genres = _genreRepository.GetAll()
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MovieViewModel m)
        {
            m.Movie.movie_id = _movieRepository.GetNewMovieId();
            m.Movie.cover_image = new byte[] {};

            foreach (string genre in m.MovieGenres)
            {
                m.Movie.Movie_Genre.Add(new Movie_Genre() { genre_name = genre, movie_id = m.Movie.movie_id });
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Er is een fout opgetreden");
                return View(m);
            }
            _movieRepository.CreateMovie(m.Movie);
            return RedirectToAction("Movie", "Movie", new { id = m.Movie.movie_id});
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            MovieViewModel m = new MovieViewModel()
            {
                Movie = _movieRepository.GetMovie(id),
                Genres = _genreRepository.GetAll()
            };
            return View(m);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MovieViewModel m)
        {
            m.Movie.price = (decimal) m.Movie.price;
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Er is een fout opgetreden");
                return View(m);
            }
            //foreach (string genre in m.MovieGenres)
            //{
            //    m.Movie.Movie_Genre.Add(new Movie_Genre() { genre_name = genre, movie_id = m.Movie.movie_id });
            //}
            _movieRepository.UpdateMovie(m.Movie);
            return RedirectToAction("Movie", "Movie", new { id = m.Movie.movie_id });
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
