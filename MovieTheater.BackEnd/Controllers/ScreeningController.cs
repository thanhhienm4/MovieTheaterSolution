using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.ScreeningServices;
using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreeningController : Controller
    {
        private readonly IScreeningService _screeningService;
        public ScreeningController(IScreeningService screeningService)
        {
            _screeningService = screeningService;
        }

        [HttpPost("Create")]
        public async Task<ApiResultLite> CreateAsync(ScreeningCreateRequest model)
        {
            var result = await _screeningService.CreateAsync(model);
            return result;
        }

        [HttpPost("Update")]
        public async Task<ApiResultLite> UpdateAsync(ScreeningUpdateRequest request)
        {
            var result = await _screeningService.UpdateAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            var result = await _screeningService.DeleteAsync(id);
            return result;
        }

        [HttpGet("GetScreeningPaging")]
        public async Task<PageResult<ScreeningVMD>> GetScreeningPagingAsync([FromQuery] ScreeningPagingRequest request)
        {
            var result = await _screeningService.GetScreeningPagingAsync(request);
            return result;
        }
    }
}
