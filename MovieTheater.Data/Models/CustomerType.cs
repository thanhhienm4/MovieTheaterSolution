using System;
using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class CustomerType
    {
        public CustomerType()
        {
            TicketPrices = new HashSet<TicketPrice>();
            Tickets = new HashSet<Ticket>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }

        public virtual ICollection<TicketPrice> TicketPrices { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}