using System;

namespace MovieTheater.Models.Price.TicketPrice
{
    public class TicketPriceVmd
    {
        public string CustomerType { get; set; }
        public string CustomerTypeName { get; set; }
        public string AuditoriumFormat { get; set; }
        public string AuditoriumFormatName { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        public decimal Price { get; set; }
        public string TimeId { get; set; }
        public string TimeName { get; set; }
        public int Id { get; set; }

        public TicketPriceVmd(Data.Models.TicketPrice ticketPrice)
        {
            Id = ticketPrice.Id;
            AuditoriumFormat = ticketPrice.AuditoriumFormat;
            TimeId = ticketPrice.TimeId;
            Price = ticketPrice.Price;
            AuditoriumFormatName = ticketPrice.AuditoriumFormatNavigation.Name;
            CustomerType = ticketPrice.CustomerType;
            ToTime = ticketPrice.ToTime;
            FromTime = ticketPrice.FromTime;
            CustomerTypeName = ticketPrice.CustomerType;
            TimeName = ticketPrice.Time.Name;
        }
    }
}