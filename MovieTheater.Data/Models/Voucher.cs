using System;
using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class Voucher
    {
        public Voucher()
        {
            //Reservations = new HashSet<Reservation>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Limit { get; set; }
        public int? Count { get; set; }
        public decimal? Value { get; set; }
        public string Operator { get; set; }
        public decimal MaxValue { get; set; }
        public bool? Auto { get; set; }

        //public virtual ICollection<Reservation> Reservations { get; set; }
    }
}