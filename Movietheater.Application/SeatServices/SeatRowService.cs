using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Infra.Seat;
using MovieTheater.Models.Infra.Seat.SeatRow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.SeatServices
{
    public class SeatRowService :ISeatRowService
    {
        private readonly MovieTheaterDBContext _context;
        public SeatRowService(MovieTheaterDBContext context)
        {
            _context = context;
        }
        public async Task<ApiResultLite> CreateAsync(string name)
        {
            SeatRow seatRow = new SeatRow()
            {
                Name = name
            };
            _context.SeatRows.Add(seatRow);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResultLite("Tạo mới thất bại");
            }

            return new ApiSuccessResultLite();

        }

        public async Task<ApiResultLite> UpdateAsync(SeatRowUpdateRequest request)
        {
            SeatRow seat = await _context.SeatRows.FindAsync(request.Id);
            if (seat == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else
            {
                seat.Name = request.Name;
                _context.Update(seat);
                if (await _context.SaveChangesAsync() != 0)
                    return new ApiSuccessResultLite("Cập nhật thành công");
                else
                    return new ApiErrorResultLite("Cập nhật thất bại");
            }
        }

        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            SeatRow seat = await _context.SeatRows.FindAsync(id);
            if (seat == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else
            {
                _context.SeatRows.Remove(seat);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResultLite("Xóa thành công");
                }
                else
                {
                    return new ApiErrorResultLite("Xóa thất bại");
                }


            }
        }

        public async Task<ApiResult<List<SeatRowVMD>>> GetAllSeatRows()
        {
            var result = await _context.SeatRows.Select(x => new SeatRowVMD()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return new ApiSuccessResult<List<SeatRowVMD>>(result);
        }

        public async Task<ApiResult<PageResult<SeatRowVMD>>> GetSeatRowPagingAsync(SeatRowPagingRequest request)
        {
            var seatRow = _context.SeatRows.Select(x => x);

            int totalRow = await seatRow.CountAsync();
            var item = seatRow.OrderBy(x => x.Name).Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new SeatRowVMD()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();

            var pageResult = new PageResult<SeatRowVMD>()
            {

                TotalRecord = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Item = item,
            };

            return new ApiSuccessResult<PageResult<SeatRowVMD>>(pageResult);
        }

        public async Task<ApiResult<SeatRowVMD>> GetSeatRowById(int id)
        {
            SeatRow seatRow = await _context.SeatRows.FindAsync(id);
            if (seatRow == null)
            {
                return new ApiErrorResult<SeatRowVMD>("Không tìm thấy");
            }
            else
            {
                var result = new SeatRowVMD()
                {
                    Id = seatRow.Id,
                    Name = seatRow.Name
                };
                return new ApiSuccessResult<SeatRowVMD>(result);
            }
        }
    }
}
