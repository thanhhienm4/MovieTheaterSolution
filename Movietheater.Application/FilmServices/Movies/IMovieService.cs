using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.Application.FilmServices.Movies
{
    public interface IMovieService
    {
        Task<ApiResult<bool>> CreateAsync(MovieCreateRequest model);

        Task<ApiResult<bool>> UpdateAsync(MovieUpdateRequest model);

        Task<ApiResult<bool>> DeleteAsync(string id);

        Task<ApiResult<PageResult<MovieVMD>>> GetPagingAsync(FilmPagingRequest request);

        Task<ApiResult<MovieMD>> GetById(string id);

        Task<ApiResult<MovieVMD>> GetFilmVMDById(string id);

        Task<ApiResult<List<MovieVMD>>> GetAllAsync();

        //Task<ApiResult<bool>> GenreAssignAsync(GenreAssignRequest request);

        Task<ApiResult<List<MovieVMD>>> GetAllPlayingAsync();

        Task<ApiResult<List<MovieVMD>>> GetAllUpcomingAsync();

        //Task<ApiResult<bool>> PosAssignAsync(PosAssignRequest request);

        //Task<ApiResult<bool>> DeletePosAssignAsync(PosAssignRequest request);

        //Task<ApiResult<List<JoiningPosVMD>>> GetJoiningAsync(int id);

        //ban service
    }
}