using System;
using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class Screening
    {
        public Screening()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public bool Active { get; set; }
        public string MovieId { get; set; }
        public string AuditoriumId { get; set; }

        public virtual Auditorium Auditorium { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
