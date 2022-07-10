using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.ScreeningServices;
using MovieTheater.Application.UserServices;
using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class KindOfScreeningController : BaseController
    {
        private readonly IkindOfScreeningService _kindOfScreeningService;
        private readonly IUserService _userService;

        public KindOfScreeningController(IkindOfScreeningService kindOfScreeningService, IUserService userService) : base(userService)
        {
            _kindOfScreeningService = kindOfScreeningService;
        }

        [HttpPost("Create")]
        public async Task<ApiResult<bool>> CreateAsync(KindOfScreeningCreateRequest request)
        {
            var result = await _kindOfScreeningService.CreateAsync(request);
            return result;
        }

        [HttpPut("Update")]
        public async Task<ApiResult<bool>> UpdateAsync(KindOfScreeningUpdateRequest request)
        {
            var result = await _kindOfScreeningService.UpdateAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            var result = await _kindOfScreeningService.DeleteAsync(id);
            return result;
        }


        [HttpGet("getAllKindOfScreening")]
        public async Task<ApiResult<List<KindOfScreeningVMD>>> GetAllBanAsync()
        {
            var result = await _kindOfScreeningService.GetAllKindOfScreeningAsync();
            return result;
        }
        [HttpGet("GetKindOfScreeningById/{id}")]
        public async Task<ApiResult<KindOfScreeningVMD>> GetKindOfScreeningVMDById(int id)
        {
            var result = await _kindOfScreeningService.GetKindOfScreeningVMDByIdAsync(id);
            return result;
        }
        


    }
}