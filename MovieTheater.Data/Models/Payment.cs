using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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