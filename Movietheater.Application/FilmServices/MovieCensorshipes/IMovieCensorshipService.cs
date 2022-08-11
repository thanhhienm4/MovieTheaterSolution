using MovieTheater.Models.Catalog.Film.MovieCensorships;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.Application.FilmServices.MovieCensorshipes
{
    public interface IMovieCensorshipService
    {
        Task<ApiResult<bool>> CreateAsync(MovieCensorshipCreateRequest request);

        Task<ApiResult<bool>> UpdateAsync(MovieCensorshipUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<ApiResult<List<MovieCensorshipVMD>>> GetAllBanAsync();
    }
}