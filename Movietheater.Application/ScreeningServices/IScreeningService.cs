using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movietheater.Application.ScreeningServices
{
    public interface IScreeningService
    {
        Task<ApiResult<bool>> CreateAsync(ScreeningCreateRequest request);

        Task<ApiResult<bool>> UpdateAsync(ScreeningUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<ApiResult<ScreeningMD>> GetScreeningMDByIdAsync(int id);

        Task<ApiResult<ScreeningVMD>> GetScreeningVMDByIdAsync(int id);

        Task<ApiResult<PageResult<ScreeningVMD>>> GetScreeningPagingAsync(ScreeningPagingRequest request);

        Task<PageResult<FilmScreeningVMD>> GetScreeningTimePagingAsync(ScreeningPagingRequest request);

        Task<ApiResult<List<FilmScreeningVMD>>> GetFilmScreeningInday(DateTime? date);

        Task<ApiResult<ScreeningOfFilmInWeekVMD>> GetListCreeningOfFilmInWeek(int filmId);
    }
}