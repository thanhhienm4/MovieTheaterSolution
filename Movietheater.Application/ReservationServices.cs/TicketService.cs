﻿using MovieTheater.Data.EF;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.ReservationService.cs
{
    public class TicketService : ITicketService
    {
        private readonly MovieTheaterDBContext _context;
        public TicketService(MovieTheaterDBContext context)
        {
            _context = context;
        }
        public Task<ApiResultLite> CreateAsync(TicketCreateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResultLite> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResultLite> UpdateAsync(TicketUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}