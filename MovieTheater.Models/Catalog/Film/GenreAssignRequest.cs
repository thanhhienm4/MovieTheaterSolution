using MovieTheater.Models.Common;
using System.Collections.Generic;

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