using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movietheater.Application.SeatServices
{
    public interface ISeatService
    {
        Task<ApiResult<bool>> CreateAsync(SeatCreateRequest request);

        Task<ApiResult<bool>> UpdateAsync(SeatUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<ApiResult<SeatVMD>> GetSeatById(int id);

        Task<ApiResult<List<SeatVMD>>> GetSeatInRoomAsync(int roomId);

        Task<ApiResult<bool>> UpdateSeatInRoomAsync(SeatsInRoomUpdateRequest request);

        Task<ApiResult<List<SeatVMD>>> GetListSeatReserved(int screeningId);
    }
}