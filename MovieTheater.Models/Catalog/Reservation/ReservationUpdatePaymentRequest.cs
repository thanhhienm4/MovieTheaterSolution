using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Reservation
{
    public class ReservationUpdatePaymentRequest
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}