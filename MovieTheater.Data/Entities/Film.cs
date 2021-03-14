using System;
using System.Collections.Generic;

namespace MovieTheater.Data.Entities
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public int Length { get; set; }

        public int BanId { get; set; }
        public Ban Ban { get; set; }

        public List<FilmInGenre> FilmInGenres { get; set; }
        public List<Screening> Screenings { get; set; }
        public List<Joining> Joinings { get; set; }
    }
}