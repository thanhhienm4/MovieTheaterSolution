using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Reservation
{
    public class ReservationVMD
    {
        public int Id { get; set; }
        public bool Paid { get; set; }
        public bool Active { get; set; }
        public string ReservationType { get; set; }
        public string Customer { get; set; }
        public string Employee { get; set; }
        public DateTime Time { get; set; } 
        public List<TicketVMD> Tickets { get; set; }
    }
}
