using System;
using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class ReservationPerson
    {
        public string CustomerTypeId { get; set; }
        public int Number { get; set; }
        public int ReservationId { get; set; }
        public int Id { get; set; }

        public virtual CustomerType CustomerType { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}
