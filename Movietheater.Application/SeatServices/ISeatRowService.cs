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
        Task<ApiResultLite> CreateAsync(SeatRowCreateRequest request);

        Task<ApiResultLite> UpdateAsync(SeatRowUpdateRequest request);

        Task<ApiResultLite> DeleteAsync(int id);

        Task<ApiResult<List<SeatRowVMD>>> GetAllSeatRows();

        Task<ApiResult<PageResult<SeatRowVMD>>> GetSeatRowPagingAsync(SeatRowPagingRequest request);

        Task<ApiResult<SeatRowVMD>> GetSeatRowById(int id);
    }
}