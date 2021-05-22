using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System.Threading.Tasks;

namespace Movietheater.Application.ReservationServices
{
    public interface ITicketService
    {
        Task<ApiResultLite> CreateAsync(TicketCreateRequest request);

        Task<ApiResultLite> DeleteAsync(int id);

        Task<ApiResultLite> UpdateAsync(TicketUpdateRequest request);
    }
}