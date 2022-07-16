using System.Collections.Generic;

namespace MovieTheater.Models.Infra.Seat
{
    public class SeatsInRoomUpdateRequest
    {
        public List<SeatCreateRequest> Seats { get; set; }
        public string AuditoriumId { get; set; }
    }
}