namespace MovieTheater.Models.Catalog.Reservation
{
    public class ReservationUpdateRequest
    {
        public int Id { get; set; }
        public string Paid { get; set; }
        public bool Active { get; set; }
    }
}