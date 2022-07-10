using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System.Threading.Tasks;

namespace MovieTheater.Application.ReservationServices
{
    public interface IReservationTypeService
    {
        Task<ApiResult<bool>> CreateAsync(string name);

        Task<ApiResult<bool>> UpdateAsync(ReservationTypeUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);
    }
}