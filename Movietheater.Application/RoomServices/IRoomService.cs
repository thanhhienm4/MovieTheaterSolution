using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.RoomModels;
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
    }
}
