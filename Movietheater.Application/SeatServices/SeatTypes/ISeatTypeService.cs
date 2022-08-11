using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat.KindOfSeat;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.Application.SeatServices.SeatTypes
{
    public interface ISeatTypeService
    {
        Task<ApiResult<bool>> CreateAsync(SeatTypeCreateRequest request);

        Task<ApiResult<bool>> UpdateAsync(KindOfSeatUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<ApiResult<List<SeatTypeVMD>>> GetAllAsync();

        Task<ApiResult<SeatTypeVMD>> GetByIdAsync(int id);
    }
}