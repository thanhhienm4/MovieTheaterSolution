﻿using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Data.Enums;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.WebApp.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ReservationApiClient _reservationApiClient;
        public ReservationController(ReservationApiClient ReservationApiClient)
        {
            _reservationApiClient = ReservationApiClient;
        }
      
       

        [HttpPost]
        public async Task<ApiResultLite> Create(ReservationCreateRequest request)
        {
            
            //can fix
            //request.EmployeeId = GetUserId();
            request.ReservationTypeId = 1;
    
            var result = await _reservationApiClient.CreateAsync(request);
            return result;
        }

       
        
    }
}
