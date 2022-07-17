using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.RoomServices;
using MovieTheater.Application.UserServices;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Infra.RoomModels;
using MovieTheater.Models.Infra.RoomModels.Format;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieTheater.Application.RoomServices.Auditoriums;
using MovieTheater.Common.Constants;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuditoriumController : BaseController
    {
        private readonly IAuditoriumService _roomService;

        public AuditoriumController(IAuditoriumService roomService, IUserService userService) : base(userService)
        {
            _roomService = roomService;
        }

        [HttpPost(APIConstant.AuditoriumCreate)]
        public async Task<ApiResult<bool>> CreateAsync(AuditoriumCreateRequest model)
        {
            var result = await _roomService.CreateAsync(model);
            return result;
        }

        [HttpPost(APIConstant.AuditoriumUpdate)]
        public async Task<ApiResult<bool>> UpdateAsync(AuditoriumUpdateRequest request)
        {
            var result = await _roomService.UpdateAsync(request);
            return result;
        }

        [HttpDelete(APIConstant.AuditoriumDelete + "/{id}")]
        public async Task<ApiResult<bool>> DeleteAsync( string id)
        {
            var result = await _roomService.DeleteAsync(id);
            return result;
        }

        [HttpPost(APIConstant.AuditoriumGetPaging)]
        public async Task<ApiResult<PageResult<AuditoriumVMD>>> GetPagingAsync(AuditoriumPagingRequest request)
        {
            var result = await _roomService.GetPagingAsync(request);
            return result;
        }

        [HttpGet(APIConstant.AuditoriumGetById)]
        public async Task<ApiResult<RoomMD>> GetByIdAsync(string id)
        {
            var result = await _roomService.GetById(id);
            return result;
        }

        [HttpGet(APIConstant.AuditoriumGetAll)]
        public async Task<ApiResult<List<AuditoriumVMD>>> GetAllAsync()
        {
            var result = await _roomService.GetAllAsync();
            return result;
        }

        [AllowAnonymous]
        [HttpGet("GetCoordinate/{id}")]
        public async Task<ApiResult<RoomCoordinate>> GetCoordinateAsync(string id)
        {
            var result = await _roomService.GetCoordinateAsync(id);
            return result;
        }
    }
}