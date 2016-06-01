using System;
using System.Collections.Generic;

namespace TheWorld.Models
{
    public partial class Movie
    {
        public Movie()
        {
            CustomerFeedback = new HashSet<CustomerFeedback>();
            Movie_Cast = new HashSet<Movie_Cast>();
            Movie_Director = new HashSet<Movie_Director>();
            Movie_Genre = new HashSet<Movie_Genre>();
            MovieAwards = new HashSet<MovieAwards>();
            Watchhistory = new HashSet<Watchhistory>();
        }

        public int movie_id { get; set; }
        public byte[] cover_image { get; set; }
        public string description { get; set; }
        public int? duration { get; set; }
        public int? minimum_age { get; set; }
        public int? previous_part { get; set; }
        public decimal price { get; set; }
        public int? publication_year { get; set; }
        public string title { get; set; }
        public string URL { get; set; }

        public virtual ICollection<CustomerFeedback> CustomerFeedback { get; set; }
        public virtual ICollection<Movie_Cast> Movie_Cast { get; set; }
        public virtual ICollection<Movie_Director> Movie_Director { get; set; }
        public virtual ICollection<Movie_Genre> Movie_Genre { get; set; }
        public virtual ICollection<MovieAwards> MovieAwards { get; set; }
        public virtual ICollection<Watchhistory> Watchhistory { get; set; }
        public virtual Movie previous_partNavigation { get; set; }
        public virtual ICollection<Movie> Inverseprevious_partNavigation { get; set; }
    }
}
