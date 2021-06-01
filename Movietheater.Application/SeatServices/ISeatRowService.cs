using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Infra.Seat;
using MovieTheater.Models.Infra.Seat.SeatRow;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movietheater.Application.SeatServices
{
    public interface ISeatRowService
    {
        Task<ApiResult<bool>> CreateAsync(SeatRowCreateRequest request);

        Task<ApiResult<bool>> UpdateAsync(SeatRowUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<ApiResult<List<SeatRowVMD>>> GetAllSeatRows();

        Task<ApiResult<PageResult<SeatRowVMD>>> GetSeatRowPagingAsync(SeatRowPagingRequest request);

        Task<ApiResult<SeatRowVMD>> GetSeatRowById(int id);
    }
}