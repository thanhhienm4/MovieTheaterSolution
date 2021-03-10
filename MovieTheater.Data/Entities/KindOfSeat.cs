using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTheater.Data.Entities
{
    public class KindOfSeat
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int Surcharge { get; set; }

        public List<Seat> Seats { get; set; }
    }
}
