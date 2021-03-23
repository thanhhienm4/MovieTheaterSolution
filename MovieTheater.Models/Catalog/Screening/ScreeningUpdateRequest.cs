using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Screening
{
    public class ScreeningUpdateRequest
    {
        public int Id { get; set; }
        public DateTime TimeStart { get; set; }
        public int Surcharge { get; set; }

        public int FilmId { get; set; }

        public int RoomId { get; set; }


        public int KindOfScreeningId { get; set; }
    }
}
