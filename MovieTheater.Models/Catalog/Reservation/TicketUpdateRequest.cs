using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Reservation
{
    public class TicketUpdateRequest
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int ScreeningId { get; set; }
        public int SeatId { get; set; }
    }
}
