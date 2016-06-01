using System;
using System.Collections.Generic;

namespace TheWorld.Models
{
    public partial class Person
    {
        public Person()
        {
            Movie_Cast = new HashSet<Movie_Cast>();
            Movie_Director = new HashSet<Movie_Director>();
        }

        public int person_id { get; set; }
        public string firstname { get; set; }
        public string gender { get; set; }
        public string lastname { get; set; }

        public virtual ICollection<Movie_Cast> Movie_Cast { get; set; }
        public virtual ICollection<Movie_Director> Movie_Director { get; set; }
    }
}
