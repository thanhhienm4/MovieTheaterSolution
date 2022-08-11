using Microsoft.EntityFrameworkCore;
using MovieTheater.Application.Helper.Extension;
using MovieTheater.Data.Models;
using MovieTheater.Models.Catalog.Price.Time;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Application.TimeServices
{
    public class
        TimeService : ITimeService
    {
        private readonly MoviesContext _context;

        public TimeService(MoviesContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateAsync(TimeCreateRequest request)
        {
            var time = await _context.Times.FindAsync(request.TimeId);
            if (time != null)
                return new ApiErrorResult<bool>("Mã đã tồn tại");

            var entity = new Time()
            {
                TimeId = request.TimeId,
                Name = request.Name,
                DateEnd = request.DateEnd,
                DateStart = request.DateStart,
                HourEnd = request.HourEnd,
                HourStart = request.HourStart,
                IsDelete = false
            };
            _context.Times.Add(entity);
            var res = await _context.SaveChangesAsync();
            if (res > 0)
                return new ApiSuccessResult<bool>(false, "Thêm thành công");
            return new ApiErrorResult<bool>("Thêm thất bại");
        }

        public async Task<ApiResult<bool>> UpdateAsync(TimeUpdateRequest request)
        {
            Time time = await _context.Times.FindAsync(request.TimeId);
            if (time == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                var entity = new Time()
                {
                    Name = request.Name,
                    DateEnd = request.DateEnd,
                    DateStart = request.DateStart,
                    HourEnd = request.HourEnd,
                    HourStart = request.HourStart,
                    IsDelete = false
                };

                _context.Times.Add(entity);
                var res = await _context.SaveChangesAsync();
                if (res > 0)
                    return new ApiSuccessResult<bool>(false, "Cập nhật thành công");
                return new ApiErrorResult<bool>("Thêm thất bại");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(string id)
        {
            Time time = await _context.Times.FindAsync(id);
            if (time == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                if (_context.TicketPrices.Count(x => x.TimeId == id) > 0)
                {
                    time.IsDelete = false;
                    _context.Update(time);
                }
                else
                {
                    _context.Remove(time);
                }

                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>(true, "Xóa thành công");
            }
        }

        public async Task<ApiResult<TimeVMD>> GetById(string id)
        {
            var time = await _context.Times.FindAsync(id);
            if (time == null)
                return new ApiErrorResult<TimeVMD>("Không tìm thấy");

            return new ApiSuccessResult<TimeVMD>(time.ToVMD());
        }

        public async Task<ApiResult<List<TimeVMD>>> GetAllAsync()
        {
            var res = await _context.Times.Select(x => x.ToVMD()).ToListAsync();

            return new ApiSuccessResult<List<TimeVMD>>(res);
        }

        public async Task<ApiResult<PageResult<TimeVMD>>> GetPagingAsync(TimePagingRequest request)
        {
            var seatRow = _context.Times.Select(x => x);

            int totalRow = await seatRow.CountAsync();
            if (request.Keyword != null)
            {
                seatRow = seatRow.Where(x => x.Name.Contains(request.Keyword)
                                             || x.TimeId.Contains(request.Keyword));
            }

            var item = seatRow.OrderBy(x => x.Name).Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => x.ToVMD()).ToList();

            var pageResult = new PageResult<TimeVMD>()
            {
                TotalRecord = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Item = item,
            };

            return new ApiSuccessResult<PageResult<TimeVMD>>(pageResult);
        }
    }
}