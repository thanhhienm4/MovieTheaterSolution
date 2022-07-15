using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.Application.FilmServices.Actors
{
    public interface IActorService
    {
        Task<ApiResult<bool>> CreateAsync(ActorCreateRequest request);

        Task<ApiResult<bool>> UpdateAsync(ActorUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<ApiResult<PageResult<ActorVMD>>> GetActorPagingAsync(ActorPagingRequest request);

        Task<ApiResult<ActorVMD>> GetById(int id);

        Task<ApiResult<List<ActorVMD>>> GetAllAsync();
    }
}