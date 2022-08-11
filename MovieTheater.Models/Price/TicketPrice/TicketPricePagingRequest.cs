using MovieTheater.Models.Common.Paging;
using System;

namespace MovieTheater.Models.Price.TicketPrice
{
    public class TicketPricePagingRequest : PagingRequest
    {
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
    }
}