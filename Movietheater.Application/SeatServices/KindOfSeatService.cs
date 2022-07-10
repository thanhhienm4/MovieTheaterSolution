using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat.KindOfSeat;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Application.SeatServices
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

            return new ApiSuccessResult<bool>(true,"Thêm thành công");
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
                    return new ApiSuccessResult<bool>(true,"Cập nhật thành công");
                else
                    return new ApiErrorResult<bool>("Cập nhật thất bại");
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
                    return new ApiSuccessResult<bool>(true,"Xóa thành công");
                }
                else
                {
                    return new ApiErrorResult<bool>("Xóa thất bại");
                }
            }
        }

        public async Task<ApiResult<List<KindOfSeatVMD>>> GetAllAsync()
        {
            var res =await _context.KindOfSeats.Select(x => new KindOfSeatVMD()
            {
                Id = x.Id,
                Name = x.Name,
                Surcharge = x.Surcharge
            }).ToListAsync();
            return new ApiSuccessResult<List<KindOfSeatVMD>>(res);
        }
        public async Task<ApiResult<KindOfSeatVMD>> GetKindOfSeatByIdAsync(int id)
        {
            var kindOfSeat = await _context.KindOfSeats.FindAsync(id);
            if (kindOfSeat == null)
                return new ApiErrorResult<KindOfSeatVMD>("Không tìm thấy loại ghế");
            else
            {
                var res =new  KindOfSeatVMD()
                {
                    Id = kindOfSeat.Id,
                    Name = kindOfSeat.Name,
                    Surcharge = kindOfSeat.Surcharge

                };
                return new ApiSuccessResult<KindOfSeatVMD>(res);
            }    
           
           
        }
    }
}