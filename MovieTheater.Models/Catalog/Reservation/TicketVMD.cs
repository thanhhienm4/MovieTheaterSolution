using System;

namespace MovieTheater.Models.Catalog.Reservation
{
    public class TicketVMD
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Film { get; set; }
        public DateTime Time { get; set; }
        public string Seat { get; set; }
        public string Room { get; set; }
        public string SeatType { get; set; }
        public string CustomerType { get; set; }
    }
}