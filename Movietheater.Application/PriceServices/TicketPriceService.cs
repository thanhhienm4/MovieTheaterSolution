using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.Models;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Price.TicketPrice;

namespace MovieTheater.Application.PriceServices
{
    public class TicketPriceService : ITicketPriceService
    {
        private readonly MoviesContext _context;

        public TicketPriceService(MoviesContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateAsync(TicketPriceCreateRequest request)
        {
            TicketPrice ticketPrice = new TicketPrice()
            {
                AuditoriumFormat = request.AuditoriumFormat,
                CustomerType = request.CustomerType,
                Price = request.Price,
                FromTime = request.FromTime,
                TimeId = request.TimeId,
                ToTime = request.ToTime
            };
            _context.TicketPrices.Add(ticketPrice);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Tạo mới thất bại");
            }
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> UpdateAsync(TicketPriceUpdateRequest request)
        {
            TicketPrice ticketPrice = await _context.TicketPrices.FindAsync(request.Id);
            if (ticketPrice == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                ticketPrice.AuditoriumFormat = request.AuditoriumFormat;
                ticketPrice.CustomerType = request.CustomerType;
                ticketPrice.FromTime = request.FromTime;
                ticketPrice.ToTime = request.ToTime;
                ticketPrice.Price = request.Price;
                ticketPrice.TimeId = request.TimeId;

                if (await _context.SaveChangesAsync() != 0)
                    return new ApiSuccessResult<bool>(true);
                return new ApiErrorResult<bool>("Cập nhật thất bại");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            TicketPrice ticketPrice = await _context.TicketPrices.FindAsync(id);
            if (ticketPrice == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                if (!_context.Seats.Any(x => x.RowId == ticketPrice.Id))
                    _context.TicketPrices.Remove(ticketPrice);
                else
                {
                    return new ApiErrorResult<bool>("Xóa thất bại");
                }

                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>(true);
            }
        }

        public async Task<ApiResult<PageResult<TicketPriceVmd>>> GetTicketPricePagingAsync(TicketPricePagingRequest request)
        {
            var ticketPrice = _context.TicketPrices.Where(x =>
                x.FromTime >= request.FromTime && x.FromTime <= x.ToTime || x.ToTime >= request.FromTime && x.FromTime <= x.ToTime);
            int totalRow = await ticketPrice.CountAsync();

            var items = ticketPrice.OrderBy(x => x.FromTime).ThenBy(
                    x => x.AuditoriumFormat)
                .ThenBy(x => x.CustomerType)
                .ThenBy(x => x.TimeId)
                .Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Include(x => x.Time)
                .Include(x => x.AuditoriumFormatNavigation)
                .Include(x => x.CustomerTypeNavigation)
                .ToList().Select(x => new TicketPriceVmd(x)).ToList();

            var pageResult = new PageResult<TicketPriceVmd>()
            {
                TotalRecord = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Item = items,
            };

            return new ApiSuccessResult<PageResult<TicketPriceVmd>>(pageResult);
        }

        public async Task<ApiResult<TicketPriceVmd>> GetTicketPriceById(int id)
        {
            TicketPrice ticketPrice = await _context.TicketPrices.FindAsync(id);
            if (ticketPrice == null)
            {
                return new ApiErrorResult<TicketPriceVmd>("Không tìm thấy");
            }
            else
            {
                var result = new TicketPriceVmd(ticketPrice);

                return new ApiSuccessResult<TicketPriceVmd>(result);
            }
        }
    }
}