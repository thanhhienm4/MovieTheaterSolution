using MovieTheater.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Screening
{
    public class ScreeningPagingRequest : PagingRequest
    {
        public DateTime Date { get; set; }
    }
}
