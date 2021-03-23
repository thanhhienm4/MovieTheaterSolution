using MovieTheater.Data.EF;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.SeatServices
{
    public class SeatService :ISeatService
    {
        private readonly MovieTheaterDBContext _context;
        public SeatService(MovieTheaterDBContext context)
        {
            _context = context;
        }

        public Task<ApiResultLite> CreateAsync(SeatCreateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResultLite> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResultLite> UpdateAsync(SeatUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
