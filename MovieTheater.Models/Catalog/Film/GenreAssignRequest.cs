using MovieTheater.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Film
{
    public class GenreAssignRequest
    {
        public int FilmId { get; set; }
        public List<SelectedItem> Genres { get; set; }

        public GenreAssignRequest()
        {
            Genres = new List<SelectedItem>();
        }
    }
}
