using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System.Threading.Tasks;

namespace MovieTheater.Application.ReservationServices
{
    public interface ITicketService
    {
        Task<ApiResult<bool>> CreateAsync(TicketCreateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<ApiResult<bool>> UpdateAsync(TicketUpdateRequest request);
    }
}