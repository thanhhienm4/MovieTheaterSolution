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