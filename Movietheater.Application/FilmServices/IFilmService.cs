using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.FilmServices
{
    public interface IFilmService
    {
        Task<ApiResultLite> CreateAsync(FilmCreateRequest model);
        Task<ApiResultLite> UpdateAsync(FilmUpdateRequest model);
        Task<ApiResultLite> DeleteAsync(int id);
        Task<ApiResult<PageResult<FilmVMD>>> GetFilmPagingAsync(FilmPagingRequest request);
        Task<ApiResult<FilmMD>> GetFilmMDById(int id);
        Task<ApiResult<FilmVMD>> GetFilmVMDById(int id);
        Task<ApiResult<List<FilmVMD>>> GetAllFilmAsync();
        Task<ApiResultLite> GenreAssignAsync(GenreAssignRequest request);
        Task<ApiResult<List<FilmVMD>>> GetAllPlayingFilmAsync();
        Task<ApiResult<List<FilmVMD>>> GetAllUpcomingFilmAsync();
        Task<ApiResultLite> PosAssignAsync(PosAssignRequest request);
        Task<ApiResultLite> DeletePosAssignAsync(PosAssignRequest request);
        Task<ApiResult<List<JoiningPosVMD>>> GetJoiningAsync(int id);

        //ban service


    }
}
