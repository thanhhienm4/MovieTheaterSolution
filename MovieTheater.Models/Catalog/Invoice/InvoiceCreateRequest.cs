using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Invoice
{
    public class InvoiceCreateRequest
    {

        public int ReservationId { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public string PaymentId { get; set; }
    }
}
