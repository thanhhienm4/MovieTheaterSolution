using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTheater.Data.Entities
{
    public class Seat
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }

        public int KindOfSeatId { get; set; }
        public KindOfSeat KindOfSeat { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public List<Ticket> tickets { get; set; }

    }
}
