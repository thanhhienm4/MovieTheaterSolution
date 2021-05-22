using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.RoomModels.Format;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movietheater.Application.RoomServices
{
    public interface IRoomFormatService
    {
        Task<ApiResultLite> CreateAsync(RoomFormatCreateRequest request);

        Task<ApiResultLite> UpdateAsync(RoomFormatUpdateRequest request);

        Task<ApiResultLite> DeleteAsync(int id);

        Task<ApiResult<List<RoomFormatVMD>>> GetAllRoomFormatAsync();
    }
}