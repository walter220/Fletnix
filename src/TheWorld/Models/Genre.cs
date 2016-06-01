using System;
using System.Collections.Generic;

namespace TheWorld.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Movie_Genre = new HashSet<Movie_Genre>();
        }

        public string genre_name { get; set; }
        public string description { get; set; }

        public virtual ICollection<Movie_Genre> Movie_Genre { get; set; }
    }
}
