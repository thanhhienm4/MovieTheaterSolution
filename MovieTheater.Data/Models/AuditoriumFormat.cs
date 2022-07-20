using System;
using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class AuditoriumFormat
    {
        public AuditoriumFormat()
        {
            Auditoriums = new HashSet<Auditorium>();
            TicketPrices = new HashSet<TicketPrice>();
            Surcharges = new HashSet<Surcharge>();

        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Auditorium> Auditoriums { get; set; }
        public virtual ICollection<TicketPrice> TicketPrices { get; set; }
        public virtual ICollection<Surcharge> Surcharges { get; set; }

    }
}