using MovieTheater.Models.Common;
using System;

namespace MovieTheater.Models.Catalog.Reservation
{
    public class ReservationPagingRequest : PagingRequest
    {
        public string userId { get; set; }
    }
}