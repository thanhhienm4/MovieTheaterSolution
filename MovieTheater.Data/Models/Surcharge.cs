using System;
using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class Surcharge
    {
        public string SeatType { get; set; }
        public string AuditoriumId { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Id { get; set; }

        public virtual Auditorium Auditorium { get; set; }
        public virtual SeatType SeatTypeNavigation { get; set; }
    }
}
