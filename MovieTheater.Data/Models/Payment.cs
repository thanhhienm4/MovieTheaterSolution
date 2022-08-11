using System.Collections.Generic;

namespace MovieTheater.Data.Models
{
    public class Payment
    {
        public Payment()
        {
            Invoices = new HashSet<Invoice>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}