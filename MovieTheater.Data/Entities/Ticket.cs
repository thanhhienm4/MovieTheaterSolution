using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTheater.Data.Entities
{
    public class Ticket
    {
        public int Price { get; set; }
        public Reservation Reservation { get; set; }
        public int ReservationId { get; set; }

        public Seat Seat { get; set; }
        public int SeatId { get; set; }

    }
}
