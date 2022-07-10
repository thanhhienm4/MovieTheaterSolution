using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.RoomModels.Format;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.Application.RoomServices
{
    public interface IRoomFormatService
    {
        Task<ApiResult<bool>> CreateAsync(RoomFormatCreateRequest request);

        Task<ApiResult<bool>> UpdateAsync(RoomFormatUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<ApiResult<List<RoomFormatVMD>>> GetAllRoomFormatAsync();
        Task<ApiResult<RoomFormatVMD>> GetRoomFormatByIdAsync(int id);
    }
}