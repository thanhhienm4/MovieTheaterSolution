using System;
using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class PaymentStastu
    {
        public PaymentStastu()
        {
            Reservations = new HashSet<Reservation>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
