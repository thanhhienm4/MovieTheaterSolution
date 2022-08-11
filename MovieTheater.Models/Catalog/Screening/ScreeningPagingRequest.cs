using MovieTheater.Models.Common.Paging;
using System;

namespace MovieTheater.Models.Catalog.Screening
{
    public class ScreeningPagingRequest : PagingRequest
    {
        public DateTime? Date { get; set; }
    }
}