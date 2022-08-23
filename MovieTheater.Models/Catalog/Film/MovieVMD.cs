using System;
using System.Collections.Generic;

namespace MovieTheater.Models.Catalog.Film
{
    public class MovieVMD
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public int Length { get; set; }
        public string TrailerURL { get; set; }
        public string Ban { get; set; }
        public string Poster { get; set; }
        public List<string> Genres { get; set; }
    }
}