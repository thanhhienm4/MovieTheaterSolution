using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Infra.Seat
{
    public class SeatCreateRequest
    {
        public int RowId { get; set; }
        public int Number { get; set; }
        public int KindOfSeatId { get; set; }
        public int RoomId { get; set; }
    }
}
