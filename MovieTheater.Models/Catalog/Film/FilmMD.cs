using System;

namespace MovieTheater.Models.Catalog.Film
{
    public class FilmMD
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public int Length { get; set; }
        public string TrailerURL { get; set; }
        public int BanId { get; set; }
        public string Poster { get; set; }
    }
}