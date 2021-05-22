using MovieTheater.Models.Catalog.Film;
using System.Collections.Generic;

namespace MovieTheater.Models.Catalog.Screening
{
    public class ScreeningOfFilmInWeekVMD
    {
        public FilmVMD Film { get; set; }
        public List<List<ScreeningMD>> Screenings { get; set; }
    }
}