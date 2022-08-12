using System;
using MovieTheater.Models.Common.Paging;

namespace MovieTheater.Models.Price.Surcharge
{
    public class SurchargePagingRequest : PagingRequest
    {
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
    }
}