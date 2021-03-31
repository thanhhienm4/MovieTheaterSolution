using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Infra.Seat.KindOfSeat
{
    public class KindOfSeatCreateRequest
    {
        public string Name { get; set; }
        public int SurCharge { get; set; }
    }
}
