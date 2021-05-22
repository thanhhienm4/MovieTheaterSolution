using MovieTheater.Models.Common;
using System;

namespace MovieTheater.Models.Catalog.Screening
{
    public class ScreeningPagingRequest : PagingRequest
    {
        public DateTime? Date { get; set; }
    }
}