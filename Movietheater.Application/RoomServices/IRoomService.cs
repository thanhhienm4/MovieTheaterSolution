using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Infra.RoomModels;
using MovieTheater.Models.Infra.RoomModels.Format;
using MovieTheater.Models.Infra.Seat;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movietheater.Application.RoomServices
{
    public interface IRoomService
    {
        Task<ApiResult<bool>> CreateAsync(RoomCreateRequest request);

        Task<ApiResult<bool>> UpdateAsync(RoomUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<PageResult<RoomVMD>> GetRoomPagingAsync(RoomPagingRequest request);

        Task<List<SeatVMD>> GetSeatsInRoom(int id);

        Task<ApiResult<RoomMD>> GetRoomById(int id);

        Task<ApiResult<List<RoomVMD>>> GetAllRoomAsync();
    }
}