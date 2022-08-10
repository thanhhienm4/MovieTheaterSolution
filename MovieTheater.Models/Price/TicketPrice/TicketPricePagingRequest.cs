using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieTheater.Models.Common.Paging;

namespace MovieTheater.Models.Price.TicketPrice
{
    public class TicketPricePagingRequest : PagingRequest
    {
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
    }
}
