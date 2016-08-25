using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models;

namespace TheWorld.ViewModels
{
    public class MovieViewModel : Movie
    {
        [Required]
        public int movie_id { get; set; }
        [Required]
        public byte[] cover_image { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public decimal price { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
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
