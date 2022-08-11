using System;

namespace MovieTheater.Models.Price.Surcharge
{
    public class SurChargeVMD
    {
        public string SeatType { get; set; }
        public string SeatTypeName { get; set; }
        public string AuditoriumFormatId { get; set; }
        public string AuditoriumFormatName { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Id { get; set; }
    }
}