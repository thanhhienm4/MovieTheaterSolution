using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Reservation
{
    public class TicketCreateRequest
    {
        public int ScreeningId { get; set; }
        public int SeatId { get; set; }
    }
}
