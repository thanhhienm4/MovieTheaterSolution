namespace MovieTheater.Models.Infra.Seat
{
    public class SeatVMD
    {
        public int Id { get; set; }
        public int RowId { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public int KindOfSeatId { get; set; }
        public int RoomId { get; set; }
    }
}