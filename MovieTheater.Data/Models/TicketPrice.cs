using System;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class TicketPrice
    {
        public string CustomerType { get; set; }
        public string AuditoriumFormat { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public decimal Price { get; set; }
        public string TimeId { get; set; }
        public int Id { get; set; }

        public virtual AuditoriumFormat AuditoriumFormatNavigation { get; set; }
        public virtual CustomerType CustomerTypeNavigation { get; set; }
        public virtual Time Time { get; set; }
    }
}