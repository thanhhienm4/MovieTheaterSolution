using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat.KindOfSeat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.SeatServices
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
            if (result == 0)
            {
                return new ApiErrorResultLite("Tạo mới thất bại");
            }

            return new ApiSuccessResultLite();

        }

        public async Task<ApiResultLite> UpdateAsync(KindOfSeatUpdateRequest request)
        {
            KindOfSeat seat = await _context.KindOfSeats.FindAsync(request.Id);
            if (seat == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else
            {
                seat.Name = request.Name;
                seat.Surcharge = request.Surcharge;
                _context.Update(seat);
                if(await _context.SaveChangesAsync()!=0)
                     return new ApiSuccessResultLite("Cập nhật thành công");
                else
                    return new ApiErrorResultLite("Cập nhật thất bại mới thất bại");
            }
        }

        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            KindOfSeat seat = await _context.KindOfSeats.FindAsync(id);
            if (seat == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else
            {
                _context.KindOfSeats.Remove(seat);
                if( await _context.SaveChangesAsync()!=0)
                {
                    return new ApiSuccessResultLite("Xóa thành công");
                }else
                {
                    return new ApiErrorResultLite("Xóa thất bại");
                }
                   
                
            }
        }
    }
}
