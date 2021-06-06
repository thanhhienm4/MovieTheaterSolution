using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat.KindOfSeat;
using System.Collections.Generic;
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

        public async Task<ApiResult<bool>> CreateAsync(KindOfSeatCreateRequest request)
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
                return new ApiErrorResult<bool>("Tạo mới thất bại");
            }

            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> UpdateAsync(KindOfSeatUpdateRequest request)
        {
            KindOfSeat seat = await _context.KindOfSeats.FindAsync(request.Id);
            if (seat == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                seat.Name = request.Name;
                seat.Surcharge = request.Surcharge;
                _context.Update(seat);
                if (await _context.SaveChangesAsync() != 0)
                    return new ApiSuccessResult<bool>(true);
                else
                    return new ApiErrorResult<bool>("Cập nhật thất bại ");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            KindOfSeat seat = await _context.KindOfSeats.FindAsync(id);
            if (seat == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                _context.KindOfSeats.Remove(seat);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResult<bool>(true);
                }
                else
                {
                    return new ApiErrorResult<bool>("Xóa thất bại");
                }
            }
        }
        //public ApiResult<List<KindOfSeat>> GetAllKindOfSeat()
        //{
        //    var res = _context.KindOfSeats.select(new )

        //}    
    }
}