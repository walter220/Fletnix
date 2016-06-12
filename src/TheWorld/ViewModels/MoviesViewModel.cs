using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models;

namespace TheWorld.ViewModels
{
    public class MoviesViewModel
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalItemCount { get; set; }

        public IEnumerable<Movie> Movies { get; set; } 

    }
}
