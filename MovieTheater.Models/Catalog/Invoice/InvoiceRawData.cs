using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Invoice
{
    public class InvoiceRawData
    {
        public int ReservationId { get; set; }
        public int InvoiceId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public string MovieId { get; set; }
        public string MovieName { get; set; }
        public string Payment { get; set; }
        public DateTime ScreeningTime { get; set; }
        public int Tickets { get; set; }

    }
}
