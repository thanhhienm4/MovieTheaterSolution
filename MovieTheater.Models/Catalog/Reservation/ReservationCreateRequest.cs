using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Reservation
{
    public class ReservationCreateRequest
    {
        public bool Paid { get; set; }
        public bool Active { get; set; }
        public int ReservationTypeId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? EmployeeId { get; set; }
    }
}
