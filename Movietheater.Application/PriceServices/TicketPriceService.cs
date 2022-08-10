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
                else
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

        public async Task<ApiResult<PageResult<TicketPriceVMD>>> GetTicketPricePagingAsync(TicketPricePagingRequest request)
        {
            var ticketPrice = _context.TicketPrices.Select(x => x);
            //    Where(x => x.from);
            int totalRow = await ticketPrice.CountAsync();
            
            var pageResult = new PageResult<TicketPriceVMD>()
            {
                TotalRecord = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Item = new List<TicketPriceVMD>(),
            };

            return new ApiSuccessResult<PageResult<TicketPriceVMD>>(pageResult);
        }

        public async Task<ApiResult<TicketPriceVMD>> GetTicketPriceById(int id)
        {
            TicketPrice TicketPrice = await _context.TicketPrices.FindAsync(id);
            if (TicketPrice == null)
            {
                return new ApiErrorResult<TicketPriceVMD>("Không tìm thấy");
            }
            else
            {
                var result = new TicketPriceVMD()
                {
                    Id = TicketPrice.Id,
                    Name = TicketPrice.Name
                };
                return new ApiSuccessResult<TicketPriceVMD>(result);
            }
        }
    }
}