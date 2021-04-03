using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTheater.Data.Entities
{
    public class SeatRow
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Seat> Seats { get; set; }
    }
}
