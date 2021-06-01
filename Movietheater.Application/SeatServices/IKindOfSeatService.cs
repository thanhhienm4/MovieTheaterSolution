using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat.KindOfSeat;
using System.Threading.Tasks;

namespace Movietheater.Application.SeatServices
{
    public interface IKindOfSeatService
    {
        Task<ApiResult<bool>> CreateAsync(KindOfSeatCreateRequest request);

        Task<ApiResult<bool>> UpdateAsync(KindOfSeatUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int Id);
    }
}