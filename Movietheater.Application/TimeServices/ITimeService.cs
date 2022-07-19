using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieTheater.Models.Catalog.Price.Time;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;

namespace MovieTheater.Application.TimeServices
{
    public interface ITimeService
    {
        Task<ApiResult<bool>> CreateAsync(TimeCreateRequest request);
        Task<ApiResult<bool>> UpdateAsync(TimeUpdateRequest request);
        Task<ApiResult<bool>> DeleteAsync(string id);
        Task<ApiResult<TimeVMD>> GetById(string id);
        Task<ApiResult<List<TimeVMD>>> GetAllAsync();
        Task<ApiResult<PageResult<TimeVMD>>> GetPagingAsync(TimePagingRequest request);

    }
}
