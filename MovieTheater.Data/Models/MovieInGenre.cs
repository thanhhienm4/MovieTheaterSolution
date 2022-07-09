using System;
using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class MovieInGenre
    {
        public string GenreId { get; set; }
        public string MovieId { get; set; }

        public virtual MovieGenre Genre { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
