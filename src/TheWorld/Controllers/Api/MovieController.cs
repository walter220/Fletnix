using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;
using TheWorld.Models.Repositories;

namespace TheWorld.Controllers.Api
{
    public class MovieController : Controller
    {
        private IMovieRepository _repository;

        public MovieController(IMovieRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("api/movies")]
        public JsonResult Get(int id)
        {
            var results = _repository.GetMovie(id);
            return Json(results);
        }
    }
}
