using System;
using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class SeatRow
    {
        public SeatRow()
        {
            Seats = new HashSet<Seat>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
    }
}
