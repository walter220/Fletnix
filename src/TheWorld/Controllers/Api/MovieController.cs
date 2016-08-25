using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;
using TheWorld.Models.Repositories;
using TheWorld.Repositories;

namespace TheWorld.Controllers.Api
{
    public class MovieController : Controller
    {
        private IMovieRepository _movie;
        private IPersonRepository _person;

        public MovieController(IMovieRepository movie, IPersonRepository person)
        {
            _movie = movie;
            _person = person;
        }

        [HttpGet("api/movies")]
        public JsonResult Get(int id)
        {
            var results = _movie.GetMovie(id);
            return Json(results);
        }

        [HttpGet("api/persons")]
        public JsonResult GetAllPersons()
        {
            var results = _person.GetPersons();
            return Json(results);
        }

        [HttpGet("api/persons-by-name")]
        public JsonResult SearchPerson([FromHeader] string name)
        {
            var results = _person.GetPersonsByName(name);
            return Json(results);
        }
    }
}
