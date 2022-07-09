using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.Application.FilmServices
{
    public interface IPeopleService
    {
        Task<ApiResult<bool>> CreateAsync(PeopleCreateRequest request);

        Task<ApiResult<bool>> UpdateAsync(PeopleUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<ApiResult<PageResult<PeopleVMD>>> GetPeoplePagingAsync(PeoplePagingRequest request);

        Task<ApiResult<PeopleVMD>> GetPeopleById(int id);

        Task<ApiResult<List<PeopleVMD>>> GetAllPeopleAsync();
    }
}