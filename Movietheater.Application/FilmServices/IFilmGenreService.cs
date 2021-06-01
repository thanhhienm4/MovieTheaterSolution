using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movietheater.Application.FilmServices
{
    public interface IFilmGenreService
    {
        Task<ApiResult<bool>> CreateAsync(string name);

        Task<ApiResult<bool>> UpdateAsync(FilmGenreUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<ApiResult<List<FilmGenreVMD>>> GetAllFilmGenreAsync();
    }
}