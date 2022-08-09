using System;

namespace MovieTheater.Data.Models
{

    public class Invoice
    {
        public int Id { get; set; }

        public int ReservationId { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public string PaymentId { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}