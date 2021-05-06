using System;
using System.Collections.Generic;

namespace MovieTheater.Data.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public bool Paid { get; set; }
        public bool Active { get; set; }
        public DateTime Time { get; set; }
        public int ReservationTypeId { get; set; }
        public ReservationType ReservationType { get; set; }

        public Guid? CustomerId { get; set; }
        public CustomerInfor Customer { get; set; }


        public Guid? EmployeeId { get; set; }
        public UserInfor Employee { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}