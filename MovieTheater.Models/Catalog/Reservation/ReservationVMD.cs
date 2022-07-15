using System;
using System.Collections.Generic;

namespace MovieTheater.Models.Catalog.Reservation
{
    public class ReservationVMD
    {
        public int Id { get; set; }
        public string Paid { get; set; }
        public bool Active { get; set; }
        public string ReservationType { get; set; }
        public string Customer { get; set; }
        public string Employee { get; set; }
        public DateTime Time { get; set; }
        public List<TicketVMD> Tickets { get; set; }

        public long TotalPrice { get; set; }
    }
}