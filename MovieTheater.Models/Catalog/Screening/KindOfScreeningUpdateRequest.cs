using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Screening
{
    public class KindOfScreeningUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Surcharge { get; set; }
    }
}
