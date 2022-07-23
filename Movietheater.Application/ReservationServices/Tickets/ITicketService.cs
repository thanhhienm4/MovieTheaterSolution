using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System.Threading.Tasks;

namespace MovieTheater.Application.ReservationServices.Tickets
{
    public interface ITicketService
    {
        Task<ApiResult<bool>> CreateAsync(TicketCreateRequest request);

        Task<ApiResult<bool>> DeleteAsync(TicketCreateRequest request);

        Task<ApiResult<bool>> UpdateAsync(TicketUpdateRequest request);
    }
}