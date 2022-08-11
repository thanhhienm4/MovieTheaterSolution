using System;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class Surcharge
    {
        public string SeatType { get; set; }
        public string AuditoriumFormatId { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Id { get; set; }

        public virtual AuditoriumFormat AuditoriumFormat { get; set; }
        public virtual SeatType SeatTypeNavigation { get; set; }
    }
}