using MovieTheater.Models.Catalog.Film;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Screening
{
    public class ScreeningOfFilmInWeekVMD
    {
        public FilmVMD Film { get; set; } 
        public List<List<ScreeningMD>> Screenings { get; set; }
    }
}
