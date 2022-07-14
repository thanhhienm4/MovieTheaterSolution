using MovieTheater.Models.Catalog.Screening;
using System.Collections.Generic;

namespace MovieTheater.Models.Catalog.Film
{
    public class FilmScreeningVMD
    {
        public MovieVMD Movie { get; set; }
        public List<ScreeningMD> ListScreening { get; set; }
    }
}