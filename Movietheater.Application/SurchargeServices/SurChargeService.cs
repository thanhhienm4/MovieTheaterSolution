using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.Models;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Price.Surcharge;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Application.SurchargeServices
{
    public class SurchargeService : ISurchargeService
    {
        private readonly MoviesContext _context;

        public SurchargeService(MoviesContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateAsync(SurchargeCreateRequest request)
        {
            Surcharge surcharge = new Surcharge()
            {
                AuditoriumFormatId = request.AuditoriumFormatId,
                EndDate = request.EndDate,
                Price = request.Price,
                StartDate = request.StartDate,
                SeatType = request.SeatType
            };
            _context.Surcharges.Add(surcharge);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Tạo mới thất bại");
            }

            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> UpdateAsync(SurchargeUpdateRequest request)
        {
            Surcharge surcharge = await _context.Surcharges.FindAsync(request.Id);
            if (surcharge == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                surcharge.AuditoriumFormatId = request.AuditoriumFormatId;
                surcharge.EndDate = request.EndDate;
                surcharge.Price = request.Price;
                surcharge.StartDate = request.StartDate;
                surcharge.SeatType = request.SeatType;

                if (await _context.SaveChangesAsync() != 0)
                    return new ApiSuccessResult<bool>(true);
                return new ApiErrorResult<bool>("Cập nhật thất bại");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            Surcharge surcharge = await _context.Surcharges.FindAsync(id);
            if (surcharge == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                if (!_context.Seats.Any(x => x.RowId == surcharge.Id))
                    _context.Surcharges.Remove(surcharge);
                else
                {
                    return new ApiErrorResult<bool>("Xóa thất bại");
                }

                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>(true);
            }
        }

        public async Task<ApiResult<PageResult<SurchargeVmd>>> GetSurchargePagingAsync(SurchargePagingRequest request)
        {
            var surcharge = _context.Surcharges.Where(x =>
                x.StartDate >= request.FromTime && x.StartDate <= x.EndDate ||
                x.EndDate >= request.ToTime && x.StartDate <= x.EndDate);
            int totalRow = await surcharge.CountAsync();

            var items = surcharge.OrderBy(x => x.StartDate).ThenBy(
                    x => x.AuditoriumFormat)
                .ThenBy(x => x.AuditoriumFormatId)
                .ThenBy(x => x.SeatType)
                .Include(x => x.AuditoriumFormat)
                .Include(x => x.SeatTypeNavigation)
                .Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .ToListAsync().Result
                .Select(x => new SurchargeVmd(x)).ToList();

            var pageResult = new PageResult<SurchargeVmd>()
            {
                TotalRecord = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Item = items,
            };

            return new ApiSuccessResult<PageResult<SurchargeVmd>>(pageResult);
        }

        public async Task<ApiResult<SurchargeVmd>> GetSurchargeById(int id)
        {
            Surcharge surcharge = await _context.Surcharges.FindAsync(id);
            if (surcharge == null)
            {
                return new ApiErrorResult<SurchargeVmd>("Không tìm thấy");
            }
            else
            {
                var result = new SurchargeVmd(surcharge);

                return new ApiSuccessResult<SurchargeVmd>(result);
            }
        }
    }
}