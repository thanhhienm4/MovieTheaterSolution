﻿using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Infra.Seat;
using MovieTheater.Models.Infra.Seat.SeatRow;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Application.SeatServices
{
    public class SeatRowService : ISeatRowService
    {
        private readonly MovieTheaterDBContext _context;

        public SeatRowService(MovieTheaterDBContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateAsync(SeatRowCreateRequest request)
        {
            SeatRow seatRow = new SeatRow()
            {
                Name = request.Name
            };
            _context.SeatRows.Add(seatRow);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Tạo mới thất bại");
            }

            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> UpdateAsync(SeatRowUpdateRequest request)
        {
            SeatRow seat = await _context.SeatRows.FindAsync(request.Id);
            if (seat == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                seat.Name = request.Name;
                _context.Update(seat);
                if (await _context.SaveChangesAsync() != 0)
                    return new ApiSuccessResult<bool>(true);
                else
                    return new ApiErrorResult<bool>("Cập nhật thất bại");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            SeatRow seatrow = await _context.SeatRows.FindAsync(id);
            if (seatrow == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                if (_context.Seats.Where(x => x.RowId == seatrow.Id).Count() == 0)
                    _context.SeatRows.Remove(seatrow);
                else
                {
                    return new ApiErrorResult<bool>("Xóa thất bại");
                }
                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>(true);
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
            if (request.Keyword != null)
            {
                seatRow = seatRow.Where(x => x.Name.Contains(request.Keyword)
                                                || x.Id.ToString().Contains(request.Keyword));
            }
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