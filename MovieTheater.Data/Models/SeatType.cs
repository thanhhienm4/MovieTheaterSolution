using System;
using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class SeatType
    {
        public SeatType()
        {
            Seats = new HashSet<Seat>();
            Surchanges = new HashSet<Surchange>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
        public virtual ICollection<Surchange> Surchanges { get; set; }
    }
}
