namespace MovieTheater.Models.Catalog.Reservation
{
    public class TicketCreateRequest
    {
        public int ScreeningId { get; set; }    
        public int SeatId { get; set; }
    }
}