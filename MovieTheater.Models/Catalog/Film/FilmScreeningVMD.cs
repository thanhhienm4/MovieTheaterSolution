using MovieTheater.Models.Catalog.Screening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Film
{
    public class FilmScreeningVMD
    {
        public FilmVMD Film { get; set; }
        public List<ScreeningMD>  ListScreening  {get ; set;}
    }
}
