using System.Collections.Generic;
using System.Threading.Tasks;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Infra.RoomModels;
using MovieTheater.Models.Infra.RoomModels.Format;
using MovieTheater.Models.Infra.Seat;

namespace MovieTheater.Application.RoomServices.Auditoriums
{
    public interface IAuditoriumService
    {
        Task<ApiResult<bool>> CreateAsync(RoomCreateRequest request);

        Task<ApiResult<bool>> UpdateAsync(AuditoriumUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<ApiResult<PageResult<RoomVMD>>> GetPagingAsync(RoomPagingRequest request);

        Task<List<SeatVMD>> GetSeats(int id);

        Task<ApiResult<RoomMD>> GetById(string id);

        Task<ApiResult<List<RoomVMD>>> GetAllAsync();

        Task<ApiResult<RoomCoordinate>> GetCoordinateAsync(string id);
    }
}