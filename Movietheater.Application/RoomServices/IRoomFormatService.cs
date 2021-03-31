using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.RoomModels.Format;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.RoomServices
{
    public interface IRoomFormatService
    {
        Task<ApiResultLite> CreateAsync(RoomFormatCreateRequest request);
        Task<ApiResultLite> UpdateAsync(RoomFormatUpdateRequest request);
        Task<ApiResultLite> DeleteAsync(int id);
        Task<List<RoomFormatVMD>> GetAllRoomFormat();
    }
}
