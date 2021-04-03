using MovieTheater.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Data.Entities
{
    public  class UserInfor
    {
        public Guid Id { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }

        public Status Status { get; set; }
        public List<Reservation> ReservationsEmployee { get; set; }
        public List<Reservation> ReservationsUser { get; set; }
        public User User { get; set; }
    }
}
