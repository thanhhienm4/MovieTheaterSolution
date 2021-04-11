using Movietheater.Application.FilmServices;
using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.ScreeningServices
{
    public interface IScreeningService 
    {
        Task<ApiResultLite> CreateAsync(ScreeningCreateRequest request);
        Task<ApiResultLite> UpdateAsync(ScreeningUpdateRequest request);
        Task<ApiResultLite> DeleteAsync(int id);
        Task<ApiResult<ScreeningVMD>> GetScreeningByIdAsync(int id);
        Task<PageResult<ScreeningVMD>> GetScreeningPagingAsync(ScreeningPagingRequest request);
        Task<PageResult<FilmScreeningVMD>> GetScreeningTimePagingAsync(ScreeningPagingRequest request);
    }
}
