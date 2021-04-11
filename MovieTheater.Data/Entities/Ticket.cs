namespace MovieTheater.Data.Entities
{
    public class Ticket
    {
        public int Price { get; set; }
        public Screening Screening { get; set; }
        public int ScreeningId { get; set; }

        public Seat Seat { get; set; }
        public int SeatId { get; set; }

        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}