using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.ScreeningServices;
using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KindOfScreeningController : Controller
    {
        private readonly IkindOfScreeningService _kindOfScreeningService;

        public KindOfScreeningController(IkindOfScreeningService kindOfScreeningService)
        {
            _kindOfScreeningService = kindOfScreeningService;
        }

        [HttpGet("getAllKindOfScreening")]
        public async Task<ApiResult<List<KindOfScreeningVMD>>> GetAllBanAsync()
        {
            var result = await _kindOfScreeningService.GetAllKindOfScreeningAsync();
            return result;
        }
    }
}