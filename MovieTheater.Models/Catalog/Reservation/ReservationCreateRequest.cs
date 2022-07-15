using System;
using System.Collections.Generic;

namespace MovieTheater.Models.Catalog.Reservation
{
    public class ReservationCreateRequest
    {
        public string Paid { get; set; }
        public bool Active { get; set; }
        public string ReservationTypeId { get; set; }
        public string CustomerId { get; set; }
        public string EmployeeId { get; set; }
        public List<TicketCreateRequest> Tickets { get; set; }
    }
}