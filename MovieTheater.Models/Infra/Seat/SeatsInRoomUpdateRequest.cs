using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Infra.Seat
{
    public class SeatsInRoomUpdateRequest
    {
        public List<SeatCreateRequest> Seats { get; set; }
        public int RoomId {get; set;}
    }
}
