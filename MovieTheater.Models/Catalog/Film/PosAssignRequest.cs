using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Film
{
    public class PosAssignRequest
    {
        public int FilmId { get; set; }
        public int PosId { get; set; }
        public int PeopleId { get; set; }
    }
}
