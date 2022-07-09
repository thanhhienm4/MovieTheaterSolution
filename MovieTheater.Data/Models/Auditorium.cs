using System;
using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class Auditorium
    {
        public Auditorium()
        {
            Screenings = new HashSet<Screening>();
            Seats = new HashSet<Seat>();
            Surchanges = new HashSet<Surchange>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string FormatId { get; set; }

        public virtual AuditoriumFormat Format { get; set; }
        public virtual ICollection<Screening> Screenings { get; set; }
        public virtual ICollection<Seat> Seats { get; set; }
        public virtual ICollection<Surchange> Surchanges { get; set; }
    }
}
