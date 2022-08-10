using MovieTheater.Models.Common;
using System;
using MovieTheater.Models.Common.Paging;

namespace MovieTheater.Models.Catalog.Screening
{
    public class ScreeningPagingRequest : PagingRequest
    {
        public DateTime? Date { get; set; }
    }
}