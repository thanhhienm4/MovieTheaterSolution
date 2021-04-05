using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieTheater.Api;
using MovieTheater.Data.Enums;
using MovieTheater.Models.Common;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat.SeatRow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    [AllowAnonymous]
    public class SeatRowController : BaseController
    {
        private readonly SeatRowApiClient _seatRowApiClient;
        public SeatRowController(SeatRowApiClient seatRowApiClient, RoleApiClient roleApiClient)
        {
            _seatRowApiClient = seatRowApiClient;
        }

        [HttpGet]
        public async Task<List<SeatRowVMD>> GetAllSeatRows()
        {
            var result = (await _seatRowApiClient.GetAllSeatRowsAsync()).ResultObj;
            return result;
        }



    }

}
