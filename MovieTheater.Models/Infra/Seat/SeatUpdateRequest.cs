using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Infra.Seat
{
    public class SeatUpdateRequest
    {
        public int Id { get; set; }
        public int RowId { get; set; }
        public int Number { get; set; }
        public int KindOfSeatId { get; set; }
        public int RoomId { get; set; }
    }
}
