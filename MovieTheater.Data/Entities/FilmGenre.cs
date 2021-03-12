using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTheater.Data.Entities
{
    public class FilmGenre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<FilmInGenre> FilmInGenres { get; set; }

    }
}
