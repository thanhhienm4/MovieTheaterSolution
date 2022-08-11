using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.Models;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat.KindOfSeat;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Application.SeatServices.SeatTypes
{
    public class SeatTypeService : ISeatTypeService
    {
        private readonly MoviesContext _context;

        public SeatTypeService(MoviesContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateAsync(SeatTypeCreateRequest request)
        {
            SeatType kindOfSeat = new SeatType()
            {
                Name = request.Name,
                Id = request.Id
            };
            _context.SeatTypes.Add(kindOfSeat);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Tạo mới thất bại");
            }

            return new ApiSuccessResult<bool>(true, "Thêm thành công");
        }

        public async Task<ApiResult<bool>> UpdateAsync(KindOfSeatUpdateRequest request)
        {
            Data.Models.SeatType seat = await _context.SeatTypes.FindAsync(request.Id);
            if (seat == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                seat.Name = request.Name;
                seat.Id = request.Id;
                _context.Update(seat);
                if (await _context.SaveChangesAsync() != 0)
                    return new ApiSuccessResult<bool>(true, "Cập nhật thành công");
                else
                    return new ApiErrorResult<bool>("Cập nhật thất bại");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            SeatType seat = await _context.SeatTypes.FindAsync(id);
            if (seat == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                _context.SeatTypes.Remove(seat);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResult<bool>(true, "Xóa thành công");
                }
                else
                {
                    return new ApiErrorResult<bool>("Xóa thất bại");
                }
            }
        }

        public async Task<ApiResult<List<SeatTypeVMD>>> GetAllAsync()
        {
            var res = await _context.SeatTypes.Select(x => new SeatTypeVMD()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
            return new ApiSuccessResult<List<SeatTypeVMD>>(res);
        }

        public async Task<ApiResult<SeatTypeVMD>> GetByIdAsync(int id)
        {
            var seatType = await _context.SeatTypes.FindAsync(id);
            if (seatType == null)
                return new ApiErrorResult<SeatTypeVMD>("Không tìm thấy loại ghế");
            else
            {
                var res = new SeatTypeVMD()
                {
                    Id = seatType.Id,
                    Name = seatType.Name,
                };
                return new ApiSuccessResult<SeatTypeVMD>(res);
            }
        }
    }
}