using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;
using TheWorld.Models.Repositories;

namespace TheWorld.Controllers.Api
{
    public class GenreController : Controller
    {
        private readonly IGenreRepository _repository;

        public GenreController(IGenreRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("api/genres")]
        public JsonResult Get()
        {
            return Json(_repository.GetAll());
        }
    }
}
