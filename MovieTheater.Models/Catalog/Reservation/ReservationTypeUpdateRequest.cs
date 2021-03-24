using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Reservation
{
    public class ReservationTypeUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
