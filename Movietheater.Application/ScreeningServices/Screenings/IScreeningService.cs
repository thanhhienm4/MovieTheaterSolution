using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.Application.ScreeningServices.Screenings
{
    public interface IScreeningService
    {
        Task<ApiResult<bool>> CreateAsync(ScreeningCreateRequest request);

        Task<ApiResult<bool>> UpdateAsync(ScreeningUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<ApiResult<ScreeningMD>> GetMDByIdAsync(int id);

        Task<ApiResult<ScreeningVMD>> GetVMDByIdAsync(int id);

        Task<ApiResult<PageResult<ScreeningVMD>>> GetScreeningPagingAsync(ScreeningPagingRequest request);
            
        Task<PageResult<MovieScreeningVMD>> GetScreeningTimePagingAsync(ScreeningPagingRequest request);

        Task<ApiResult<List<MovieScreeningVMD>>> GetFilmScreeningInday(DateTime? date);

        Task<ApiResult<ScreeningOfFilmInWeekVMD>> GetListOfMovieInWeek(string movieId);
    }
}