using System;
using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public class Reservation
    {
        public Reservation()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string PaymentStatus { get; set; }
        public bool Active { get; set; }
        public DateTime Time { get; set; }
        public string TypeId { get; set; }
        public string Customer { get; set; }
        public string EmployeeId { get; set; }
        //public string VoucherId { get; set; }
        public int ScreeningId { get; set; }

        public virtual Customer CustomerNavigation { get; set; }
        public virtual staff Employee { get; set; }
        public virtual PaymentStatus PaymentStatusNavigation { get; set; }
        public virtual Screening Screening { get; set; }
        public virtual ReservationType Type { get; set; }
        //public virtual Voucher Voucher { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}