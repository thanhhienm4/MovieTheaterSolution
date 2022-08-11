using MovieTheater.Models.Common.Paging;

namespace MovieTheater.Models.Catalog.Reservation
{
    public class ReservationPagingRequest : PagingRequest
    {
        public string userId { get; set; }
    }
}