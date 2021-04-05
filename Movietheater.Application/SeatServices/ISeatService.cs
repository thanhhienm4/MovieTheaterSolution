using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.SeatServices
{
    public interface ISeatService
    {
        Task<ApiResultLite> CreateAsync(SeatCreateRequest request);
        Task<ApiResultLite> UpdateAsync(SeatUpdateRequest request);
        Task<ApiResultLite> DeleteAsync(int id);
        Task<ApiResult<SeatVMD>> GetSeatById(int id);
        Task<ApiResult<List<List<SeatVMD>>>> GetSeatInRoomAsync(int roomId);
        Task<ApiResultLite> UpdateSeatInRoomAsync(SeatsInRoomUpdateRequest request);

    }
}
