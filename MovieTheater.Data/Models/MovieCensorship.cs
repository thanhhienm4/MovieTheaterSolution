using System;
using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class MovieCensorship
    {
        public MovieCensorship()
        {
            Movies = new HashSet<Movie>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}