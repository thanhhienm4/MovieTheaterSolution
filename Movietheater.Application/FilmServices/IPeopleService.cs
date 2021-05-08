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
    public interface IPeopleService
    {
        Task<ApiResultLite> CreateAsync(PeopleCreateRequest request);
        Task<ApiResultLite> UpdateAsync(PeopleUpdateRequest request);
        Task<ApiResultLite> DeleteAsync(int id);
        Task<ApiResult<PageResult<PeopleVMD>>> GetPeoplePagingAsync(PeoplePagingRequest request);
        Task<ApiResult<PeopleVMD>> GetPeopleById(int id);
        Task<ApiResult<List<PeopleVMD>>> GetAllPeopleAsync();
    }
}
