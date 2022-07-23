namespace MovieTheater.Models.Catalog.Reservation
{
    public class TicketCreateRequest
    {
        public int ReservationId { get; set; }
        public int SeatId { get; set; }
        public string CustomerType { get; set; }
    }
}