using System;
using System.Collections.Generic;

namespace MovieTheater.Models.Catalog.Reservation
{
    public class ReservationCreateRequest
    {
        public bool Paid { get; set; }
        public bool Active { get; set; }
        public int ReservationTypeId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? EmployeeId { get; set; }
        public List<TicketCreateRequest> Tickets { get; set; }
    }
}