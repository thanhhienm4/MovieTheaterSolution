
using Microsoft.AspNetCore.Identity;
using MovieTheater.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTheater.Data.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public int RoleId { get; set; }

        public Status Status { get; set; }

        public List<Reservation> ReservationsEmployee { get; set; }
        public List<Reservation> ReservationsUser { get; set; }

    }
}
