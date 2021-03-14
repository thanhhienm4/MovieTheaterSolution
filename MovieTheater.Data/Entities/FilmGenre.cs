using System.Collections.Generic;

namespace MovieTheater.Data.Entities
{
    public class FilmGenre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<FilmInGenre> FilmInGenres { get; set; }
    }
}