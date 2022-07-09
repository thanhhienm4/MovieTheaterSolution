using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.Application.FilmServices
{
    public interface IFilmService
    {
        Task<ApiResult<bool>> CreateAsync(FilmCreateRequest model);

        Task<ApiResult<bool>> UpdateAsync(FilmUpdateRequest model);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<ApiResult<PageResult<FilmVMD>>> GetFilmPagingAsync(FilmPagingRequest request);

        Task<ApiResult<FilmMD>> GetFilmMDById(int id);

        Task<ApiResult<FilmVMD>> GetFilmVMDById(int id);

        Task<ApiResult<List<FilmVMD>>> GetAllFilmAsync();

        Task<ApiResult<bool>> GenreAssignAsync(GenreAssignRequest request);

        Task<ApiResult<List<FilmVMD>>> GetAllPlayingFilmAsync();

        Task<ApiResult<List<FilmVMD>>> GetAllUpcomingFilmAsync();

        Task<ApiResult<bool>> PosAssignAsync(PosAssignRequest request);

        Task<ApiResult<bool>> DeletePosAssignAsync(PosAssignRequest request);

        Task<ApiResult<List<JoiningPosVMD>>> GetJoiningAsync(int id);

        //ban service
    }
}