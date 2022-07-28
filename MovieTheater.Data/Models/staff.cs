using System;
using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class staff
    {
        public staff()
        {
            Reservations = new HashSet<Reservation>();
        }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; }

        public virtual Role RoleNavigation { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
