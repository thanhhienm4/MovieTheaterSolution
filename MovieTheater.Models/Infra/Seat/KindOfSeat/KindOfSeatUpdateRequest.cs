namespace MovieTheater.Models.Infra.Seat.KindOfSeat
{
    public class KindOfSeatUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Surcharge { get; set; }
    }
}