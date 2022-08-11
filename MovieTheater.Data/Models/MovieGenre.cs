using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class MovieGenre
    {
        public MovieGenre()
        {
            MovieInGenres = new HashSet<MovieInGenre>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MovieInGenre> MovieInGenres { get; set; }
    }
}