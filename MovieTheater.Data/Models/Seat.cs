using System;
using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class Seat
    {
        public Seat()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int RowId { get; set; }
        public int Number { get; set; }
        public string AuditoriumId { get; set; }
        public string TypeId { get; set; }
        public bool IsActive { get; set; }
        public int Id { get; set; }

        public virtual Auditorium Auditorium { get; set; }
        public virtual SeatRow Row { get; set; }
        public virtual SeatType Type { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
