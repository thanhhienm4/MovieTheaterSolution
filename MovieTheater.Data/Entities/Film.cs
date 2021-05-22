using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Data.Entities
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Length { get; set; }

        public string TrailerURL { get; set; }
        public string Poster { get; set; }

        public int BanId { get; set; }
        public Ban Ban { get; set; }

        public List<FilmInGenre> FilmInGenres { get; set; }
        public List<Screening> Screenings { get; set; }
        public List<Joining> Joinings { get; set; }
    }
}