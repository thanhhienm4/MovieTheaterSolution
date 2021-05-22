using System;
using System.Collections.Generic;

namespace MovieTheater.Data.Entities
{
    public class UserInfor
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public List<Reservation> ReservationsEmployee { get; set; }
        public User User { get; set; }
    }
}