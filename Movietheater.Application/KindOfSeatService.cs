using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat.KindOfSeat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application
{
    public class KindOfSeatService : IKindOfSeatService
    {
        private readonly MovieTheaterDBContext _context;
        public KindOfSeatService(MovieTheaterDBContext context)
        {
            _context = context;
        }
        public async Task<ApiResultLite> CreateAsync(KindOfSeatCreateRequest request)
        {
            KindOfSeat kindOfSeat = new KindOfSeat()
            {
                Name = request.Name,
                Surcharge = request.SurCharge
            };
            _context.KindOfSeats.Add(kindOfSeat);
            int result = await _context.SaveChangesAsync();
            if(result == 0)
            {
                return new ApiErrorResultLite("Tạo mới thất bại");
            }

            return new ApiSuccessResultLite();

        }
    }
}
