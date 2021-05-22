using System.Collections.Generic;

namespace MovieTheater.Data.Entities
{
    public class SeatRow
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Seat> Seats { get; set; }
    }
}