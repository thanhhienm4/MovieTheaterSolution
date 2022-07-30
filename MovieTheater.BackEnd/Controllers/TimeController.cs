using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.TimeServices;
using MovieTheater.Application.UserServices;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Catalog.Price.Time;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeController : BaseController
    {
        private readonly ITimeService _timeService;

        public TimeController(IUserService userService, ITimeService timeService) : base(userService)
        {
            _timeService = timeService;
        }

        [HttpPost(APIConstant.TimeCreate)]
        public async Task<ApiResult<bool>> CreateAsync(TimeCreateRequest request)
        {
            var result = await _timeService.CreateAsync(request);
            return result;
        }

        [HttpPut(APIConstant.TimeUpdate)]
        public async Task<ApiResult<bool>> UpdateAsync(TimeUpdateRequest request)
        {
            var result = await _timeService.UpdateAsync(request);
            return result;
        }

        [HttpDelete(APIConstant.TimeDelete + "/{id}")]
        public async Task<ApiResult<bool>> DeleteAsync(string id)
        {
            var result = await _timeService.DeleteAsync(id);
            return result;
        }

        [HttpGet(APIConstant.TimeGetById)]
        public async Task<ApiResult<TimeVMD>> GetByIdAsync(string id)
        {
            var result = await _timeService.GetById(id);
            return result;
        }

        [HttpGet(APIConstant.TimeGetAll)]
        public async Task<ApiResult<List<TimeVMD>>> GetAllAsync()
        {
            var result = await _timeService.GetAllAsync();
            return result;
        }

        [HttpPost(APIConstant.TimePaging)]
        public async Task<ApiResult<PageResult<TimeVMD>>> PagingAsync(TimePagingRequest request)
        {
            var result = await _timeService.GetPagingAsync(request);
            return result;
        }
    }
}