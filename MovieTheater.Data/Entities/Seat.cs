
using System.Collections.Generic;

namespace MovieTheater.Data.Entities
{
    public class Seat
    {
        public int Id { get; set; }
        public int RowId { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }

        public int KindOfSeatId { get; set; }
        public KindOfSeat KindOfSeat { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public SeatRow SeatRow { get; set; }

        

        public bool IsActive { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}