using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.UserServices;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System.Threading.Tasks;
using MovieTheater.Application.SurchargeServices;
using MovieTheater.Models.Price.Surcharge;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurchargeController : BaseController
    {
        private readonly ISurchargeService _surchargeService;

        public SurchargeController(IUserService userService, ISurchargeService surchargeService) : base(userService)
        {
            _surchargeService = surchargeService;
        }

        [HttpPost(ApiConstant.SurchargeCreate)]
        public async Task<ApiResult<bool>> CreateAsync(SurchargeCreateRequest request)
        {
            var result = await _surchargeService.CreateAsync(request);
            return result;
        }

        [HttpPut(ApiConstant.SurchargeUpdate)]
        public async Task<ApiResult<bool>> UpdateAsync(SurchargeUpdateRequest request)
        {
            var result = await _surchargeService.UpdateAsync(request);
            return result;
        }

        [HttpDelete(ApiConstant.SurchargeDelete + "/{id}")]
        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            var result = await _surchargeService.DeleteAsync(id);
            return result;
        }

        [HttpGet(ApiConstant.SurchargeGetById)]
        public async Task<ApiResult<SurchargeVmd>> GetByIdAsync(int id)
        {
            var result = await _surchargeService.GetSurchargeById(id);
            return result;
        }

        [HttpPost(ApiConstant.SurchargePaging)]
        public async Task<ApiResult<PageResult<SurchargeVmd>>> PagingAsync(SurchargePagingRequest request)
        {
            var result = await _surchargeService.GetSurchargePagingAsync(request);
            return result;
        }
    }
}