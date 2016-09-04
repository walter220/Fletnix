using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models;

namespace TheWorld.ViewModels
{
    public class MovieViewModel
    {
        public Movie Movie { get; set; }
        public ICollection<string> MovieGenres { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}
