﻿using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.ReservationService.cs
{
    public interface ITicketService
    {
        Task<ApiResultLite> CreateAsync(TicketCreateRequest request);

        Task<ApiResultLite> DeleteAsync(int id);

        Task<ApiResultLite> UpdateAsync(TicketUpdateRequest request);
    }
}
