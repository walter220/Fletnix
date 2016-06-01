using System;
using System.Collections.Generic;

namespace TheWorld.Models
{
    public partial class Movie_Genre
    {
        public int movie_id { get; set; }
        public string genre_name { get; set; }

        public virtual Genre genre_nameNavigation { get; set; }
        public virtual Movie movie { get; set; }
    }
}
