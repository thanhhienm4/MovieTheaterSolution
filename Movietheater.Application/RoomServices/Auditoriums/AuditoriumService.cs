using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.Models;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Infra.RoomModels;
using MovieTheater.Models.Infra.RoomModels.Format;
using MovieTheater.Models.Infra.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Application.RoomServices.Auditoriums
{
    public class AuditoriumService : IAuditoriumService
    {
        private readonly MoviesContext _context;

        public AuditoriumService(MoviesContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateAsync(AuditoriumCreateRequest request)
        {
            var maxId = await _context.Auditoriums.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefaultAsync();
            string id = CreateAuditoriumId(maxId);
            if (_context.Auditoriums.Count(x => x.Name == request.Name) != 0)
                return new ApiErrorResult<bool>("Tên phòng đã bị trùng");

            var auditorium = new Auditorium()
            {
                Id = id,
                Name = request.Name,
                FormatId = request.FormatId
            };

            await _context.Auditoriums.AddAsync(auditorium);
            if (await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResult<bool>("Không thể thêm phòng");
            }

            return new ApiSuccessResult<bool>(true, "Thêm thành công");
        }

        public async Task<ApiResult<bool>> UpdateAsync(AuditoriumUpdateRequest request)
        {
            Auditorium auditorium = await _context.Auditoriums.FindAsync(request.Id);
            if (auditorium == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy phòng");
            }
            else
            {
                if (_context.Auditoriums.Count(x => x.Id != request.Id && x.Name == request.Name) != 0)
                    return new ApiErrorResult<bool>("Tên phòng đã bị trùng");

                auditorium.Name = request.Name;
                auditorium.FormatId = request.FormatId;

                _context.Auditoriums.Update(auditorium);
                int rs = await _context.SaveChangesAsync();
                if (rs == 0)
                {
                    return new ApiErrorResult<bool>("Cập nhật thất bại");
                }

                return new ApiSuccessResult<bool>(true, "Cập nhật thành công");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(string id)
        {
            Auditorium auditorium = await _context.Auditoriums.FindAsync(id);
            if (auditorium == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy phòng");
            }
            else
            {
                if (auditorium.Screenings.Any())
                {
                    return new ApiErrorResult<bool>("không thể xóa");
                }
                else
                {
                    try
                    {
                        var seats = _context.Seats.Where(x => x.AuditoriumId == auditorium.Id);
                        _context.Seats.RemoveRange(seats);
                        _context.SaveChanges();

                        _context.Auditoriums.Remove(auditorium);
                        _context.SaveChanges();

                        return new ApiSuccessResult<bool>(true, "Xóa thành công");
                    }
                    catch (DbUpdateException)
                    {
                        return new ApiErrorResult<bool>("xóa thất bại");
                    }
                }
            }
        }

        public async Task<ApiResult<PageResult<AuditoriumVMD>>> GetPagingAsync(AuditoriumPagingRequest request)
        {
            var query = from r in _context.Auditoriums
                join f in _context.AuditoriumFormats on r.FormatId equals f.Id
                select new { r, f };

            if (request.Keyword != null)
            {
                query = query.Where(x => x.r.Name.Contains(request.Keyword) ||
                                         x.r.Id.ToString().Contains(request.Keyword) ||
                                         x.f.Name.Contains(request.Keyword));
            }

            if (request.FormatId != null)
            {
                query = query.Where(x => x.r.FormatId == request.FormatId);
            }

            PageResult<AuditoriumVMD> result = new PageResult<AuditoriumVMD>();
            result.TotalRecord = await query.CountAsync();
            result.PageIndex = request.PageIndex;
            result.PageSize = request.PageSize;

            var rooms = query.Select(x => new AuditoriumVMD()
            {
                Id = x.r.Id,
                Name = x.r.Name,
                Format = x.f.Name
            }).OrderBy(x => x.Id).Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList();
            result.Item = rooms;

            return new ApiSuccessResult<PageResult<AuditoriumVMD>>(result);
        }

        public Task<List<SeatVMD>> GetSeats(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<RoomMD>> GetById(string id)
        {
            Auditorium auditorium = await _context.Auditoriums.FindAsync(id);
            if (auditorium == null)
            {
                return new ApiErrorResult<RoomMD>("Không tìm thấy phòng");
            }
            else
            {
                var result = new RoomMD()
                {
                    Id = auditorium.Id,
                    Name = auditorium.Name,
                    FormatId = auditorium.FormatId
                };
                return new ApiSuccessResult<RoomMD>(result);
            }
        }

        public async Task<ApiResult<List<AuditoriumVMD>>> GetAllAsync()
        {
            var query = from r in _context.Auditoriums
                join f in _context.AuditoriumFormats on r.FormatId equals f.Id
                select new { r, f };

            var rooms = await query.Select(x => new AuditoriumVMD()
            {
                Id = x.r.Id,
                Name = x.r.Name,
                Format = x.f.Name
            }).OrderBy(x => x.Id).ToListAsync();

            return new ApiSuccessResult<List<AuditoriumVMD>>(rooms);
        }

        public async Task<ApiResult<RoomCoordinate>> GetCoordinateAsync(string id)
        {
            RoomCoordinate coordinate = new RoomCoordinate();

            var queryRow = from sr in _context.SeatRows
                join s in _context.Seats on sr.Id equals s.RowId
                where s.IsActive == true && s.AuditoriumId == id
                select sr;

            coordinate.Bottom = await queryRow.MinAsync(x => x.Id);
            coordinate.Top = await queryRow.MaxAsync(x => x.Id);

            var queryCol = from s in _context.Seats
                where s.IsActive == true && s.AuditoriumId == id
                select s;

            coordinate.Right = await queryCol.MaxAsync(s => s.Number);
            coordinate.Left = await queryCol.MinAsync(s => s.Number);

            return new ApiSuccessResult<RoomCoordinate>(coordinate);
        }

        public string CreateAuditoriumId(string currentIndex)
        {
            if (string.IsNullOrEmpty(currentIndex))
                return "P0001";

            int number = Int32.Parse(currentIndex.Substring(2));
            number += 1;
            return $"P{number.ToString().PadLeft(4, '0')}";
        }
    }
}