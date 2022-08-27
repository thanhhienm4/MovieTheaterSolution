using MovieTheater.Data.Results;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.Application.SeatServices.Seats
{
    public interface ISeatService
    {
        Task<ApiResult<bool>> CreateAsync(SeatCreateRequest request);

        Task<ApiResult<bool>> UpdateAsync(SeatUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<ApiResult<SeatVMD>> GetSeatById(int id);

        Task<ApiResult<List<SeatVMD>>> GetAllInRoomAsync(string auditoriumId);

        Task<ApiResult<bool>> UpdateInRoomAsync(SeatsInRoomUpdateRequest request);

        Task<ApiResult<List<SeatModel>>> GetListReserve(int screeningId);
    }
}