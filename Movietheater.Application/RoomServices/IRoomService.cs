using MovieTheater.Models.Common;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Identity.Role;
using MovieTheater.Models.Infra.RoomModels;
using MovieTheater.Models.Infra.RoomModels.Format;
using MovieTheater.Models.Infra.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.RoomServices
{
    public interface IRoomService
    {
        Task<ApiResultLite> CreateAsync(RoomCreateRequest request);
        Task<ApiResultLite> UpdateAsync(RoomUpdateRequest request);
        Task<ApiResultLite> DeleteAsync(int id);
        Task<PageResult<RoomVMD>> GetRoomPagingAsync(RoomPagingRequest request);
        Task<List<SeatVMD>> GetSeatsInRoom(int id);
        Task<ApiResult<RoomMD>> GetRoomById(int id);
        Task<ApiResult<List<RoomVMD>>> GetAllRoomAsync();
    }
}
