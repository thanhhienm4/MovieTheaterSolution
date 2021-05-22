using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movietheater.Application.SeatServices
{
    public interface ISeatService
    {
        Task<ApiResultLite> CreateAsync(SeatCreateRequest request);

        Task<ApiResultLite> UpdateAsync(SeatUpdateRequest request);

        Task<ApiResultLite> DeleteAsync(int id);

        Task<ApiResult<SeatVMD>> GetSeatById(int id);

        Task<ApiResult<List<SeatVMD>>> GetSeatInRoomAsync(int roomId);

        Task<ApiResultLite> UpdateSeatInRoomAsync(SeatsInRoomUpdateRequest request);

        Task<ApiResult<List<SeatVMD>>> GetListSeatReserved(int screeningId);
    }
}