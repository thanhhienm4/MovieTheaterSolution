using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.FilmServices.Actors;
using MovieTheater.Application.UserServices;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PeopleController : BaseController
    {
        private readonly IActorService _actorService;
        private readonly IUserService _customerService;
        public PeopleController(IActorService actorService, IUserService customerService) : base(customerService)
        {
            _actorService = actorService;
            _customerService = customerService;
        }

        [HttpPost("Create")]
        public async Task<ApiResult<bool>> CreateAsync(ActorCreateRequest request)
        {
            var result = await _actorService.CreateAsync(request);
            return result;
        }

        [HttpPut("Update")]
        public async Task<ApiResult<bool>> UpdateAsync(ActorUpdateRequest request)
        {
            var result = await _actorService.UpdateAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            var result = await _actorService.DeleteAsync(id);
            return result;
        }

        [HttpPost("GetPeoplePaging")]
        public async Task<ApiResult<PageResult<ActorVMD>>> GetPeoplePaging(ActorPagingRequest request)
        {
            var result = await _actorService.GetActorPagingAsync(request);
            return result;
        }

        [HttpGet("GetById/{id}")]
        public async Task<ApiResult<ActorVMD>> GetPeopleByIdAsync(int id)
        {
            var result = await _actorService.GetById(id);
            return result;
        }

        [HttpGet("GetAllPeople")]
        public async Task<ApiResult<List<ActorVMD>>> GetAllPeopleAsync()
        {
            var result = await _actorService.GetAllAsync();
            return result;
        }
    }
}