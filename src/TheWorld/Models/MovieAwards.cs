using System;
using System.Collections.Generic;

namespace TheWorld.Models
{
    public partial class MovieAwards
    {
        public int movie_id { get; set; }
        public string award { get; set; }
        public string result { get; set; }
        public string number_of_awards { get; set; }

        public virtual Movie movie { get; set; }
    }
}
