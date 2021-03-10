using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTheater.Data.Entities
{
    public class KindOfFilm
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Film> Films { get; set; }
    }
}
