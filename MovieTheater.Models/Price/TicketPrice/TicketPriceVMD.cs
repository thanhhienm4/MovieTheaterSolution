using System;
using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.Price.TicketPrice
{
    public class TicketPriceVMD
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
    }
}