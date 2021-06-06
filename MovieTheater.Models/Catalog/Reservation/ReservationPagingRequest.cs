using MovieTheater.Models.Common;
using System;

namespace MovieTheater.Models.Catalog.Reservation
{
    public class ReservationPagingRequest : PagingRequest
    {
        public Guid? userId { get; set; }
    }
}