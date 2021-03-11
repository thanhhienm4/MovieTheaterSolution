using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTheater.Data.Entities
{
    public class Screening
    {
        public int Id { get; set; }
        public DateTime TimeStart { get; set; }
        public int Surcharge { get; set; }

        public int FilmId { get; set; }
        public Film Film { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public List<Reservation> ReservationsP { get; set; }
    }
}
