using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class SeatType
    {
        public SeatType()
        {
            Seats = new HashSet<Seat>();
            Surcharges = new HashSet<Surcharge>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
        public virtual ICollection<Surcharge> Surcharges { get; set; }
    }
}