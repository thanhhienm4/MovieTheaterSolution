using System;
using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class Movie
    {
        public Movie()
        {
            MovieInGenres = new HashSet<MovieInGenre>();
            Screenings = new HashSet<Screening>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public int Length { get; set; }
        public string TrailerUrl { get; set; }
        public string Poster { get; set; }
        public string CensorshipId { get; set; }

        public virtual MovieCensorship Censorship { get; set; }
        public virtual ICollection<MovieInGenre> MovieInGenres { get; set; }
        public virtual ICollection<Screening> Screenings { get; set; }
    }
}