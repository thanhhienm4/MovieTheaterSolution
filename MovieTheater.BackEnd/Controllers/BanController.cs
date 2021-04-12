using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.FilmServices;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BanController : Controller
    {
        private readonly IBanService _banService;

        public BanController(IBanService banService)
        {
            _banService = banService;
        }
        [HttpGet("getAllBan")]
        public async Task<ApiResult<List<BanVMD>>> GetAllBanAsync()
        {
            var result =await  _banService.GetAllBanAsync();
            return result;
        }
    }
}
