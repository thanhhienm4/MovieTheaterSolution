﻿using System;
using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class AuditoriumFormat
    {
        public AuditoriumFormat()
        {
            Auditoria = new HashSet<Auditorium>();
            TicketPrices = new HashSet<TicketPrice>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Auditorium> Auditoria { get; set; }
        public virtual ICollection<TicketPrice> TicketPrices { get; set; }
    }
}
