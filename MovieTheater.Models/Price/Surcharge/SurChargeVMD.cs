using System;

namespace MovieTheater.Models.Price.Surcharge
{
    public class SurchargeVmd
    {
        public string SeatType { get; set; }
        public string SeatTypeName { get; set; }
        public string AuditoriumFormatId { get; set; }
        public string AuditoriumFormatName { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Id { get; set; }

        public SurchargeVmd(Data.Models.Surcharge surcharge)
        {
            if (surcharge == null)
                return;

            SeatType = surcharge.SeatType;
            Id = surcharge.Id;
            Price = surcharge.Price;
            AuditoriumFormatId = surcharge.AuditoriumFormatId;
            AuditoriumFormatName = surcharge.AuditoriumFormat?.Name;
            StartDate = surcharge.StartDate;
            EndDate = surcharge.EndDate;
            Price = surcharge.Price;
            SeatType = surcharge.SeatType;
            SeatTypeName = surcharge.SeatTypeNavigation?.Name;
        }
    }
}