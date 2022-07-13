using MovieTheater.Models.Catalog.Film.MovieGenres;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.Application.FilmServices.MovieGenres
{
    public interface IMovieGenreService
    {
        Task<ApiResult<bool>> CreateAsync(MovieGenreCreateRequest request);

        Task<ApiResult<bool>> UpdateAsync(MovieGenreUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<ApiResult<List<MovieGenreVMD>>> GetAllMovieGenreAsync();
    }
}