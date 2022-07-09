using System;
using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public int SeatId { get; set; }
        public int Price { get; set; }
        public int? ReservationId { get; set; }

        public virtual Reservation Reservation { get; set; }
        public virtual Seat Seat { get; set; }
    }
}
