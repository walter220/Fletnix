using System;
using System.Collections.Generic;

namespace TheWorld.Models
{
    public partial class Movie_Cast
    {
        public int movie_id { get; set; }
        public int person_id { get; set; }
        public string role { get; set; }

        public virtual Movie movie { get; set; }
        public virtual Person person { get; set; }
    }
}
