using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Reservation
{
    public class ReservationUpdateRequest
    {
        public int Id { get; set; }
        public bool Paid { get; set; }
        public bool Active { get; set; }

    }
}
