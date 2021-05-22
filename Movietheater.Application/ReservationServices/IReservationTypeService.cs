using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System.Threading.Tasks;

namespace Movietheater.Application.ReservationServices
{
    public interface IReservationTypeService
    {
        Task<ApiResultLite> CreateAsync(string name);

        Task<ApiResultLite> UpdateAsync(ReservationTypeUpdateRequest request);

        Task<ApiResultLite> DeleteAsync(int id);
    }
}