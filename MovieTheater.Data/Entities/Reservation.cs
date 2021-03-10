﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTheater.Data.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public bool Paid { get; set; }
        public bool Active { get; set; }

        public int ReservationTypeId { get; set; }
        public ReservationType ReservationType { get; set; }

        public int? UserId { get; set; }
        public AppUser User { get; set; }

        public int ScreeningId { get; set; }
        public Screening Screening { get; set; }

        public int? EmployeeId { get; set; }
        public AppUser Employee { get; set; }

        public List<Ticket> tickets { get; set; }

    }
}
