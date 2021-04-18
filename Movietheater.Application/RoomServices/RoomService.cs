﻿using MovieTheater.Data.EF;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.RoomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Identity.Role;
using MovieTheater.Models.Common;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Models.Infra.RoomModels.Format;
using MovieTheater.Models.Infra.Seat;

namespace Movietheater.Application.RoomServices
{

    public class RoomService : IRoomService
    {
        private readonly MovieTheaterDBContext _context;

        public RoomService(MovieTheaterDBContext context)
        {
            _context = context;
        }

        public async Task<ApiResultLite> CreateAsync(RoomCreateRequest model)
        {
            var room = new Room()
            {
                Name = model.Name,
                FormatId = model.FormatId
            };

            await _context.Rooms.AddAsync(room);
            if (await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResultLite("Không thể thêm phòng");
            }
            return new ApiSuccessResultLite("Thêm thành công");

        }
        public async Task<ApiResultLite> UpdateAsync(RoomUpdateRequest model)
        {
            Room room = await _context.Rooms.FindAsync(model.Id);
            if (room == null)
            {
                return new ApiErrorResultLite("Không tìm thấy phòng");
            }
            else
            {
                room.Name = model.Name;
                room.FormatId = model.FormatId;


                _context.Rooms.Update(room);
                int rs = await _context.SaveChangesAsync();
                if (rs == 0)
                {
                    return new ApiErrorResultLite("Cập nhật thất bại");
                }
                return new ApiSuccessResultLite("Cập nhật thành công");

            }
        }
        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return new ApiErrorResultLite("Không tìm thấy phòng");
            }
            else
            {
                _context.Rooms.Remove(room);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResultLite("Xóa thành công");
                }
                else
                {
                    return new ApiSuccessResultLite("Không xóa được");
                }
            }
        }

        public async Task<PageResult<RoomMD>> GetRoomPagingAsync(RoomPagingRequest request)
        {
            var query = from r in _context.Rooms
                        join f in _context.RoomFormats on r.FormatId equals f.Id
                        select new { r, f };

            if (request.Keyword != null)
            {
                query = query.Where(x => x.r.Name.Contains(request.Keyword) ||
                x.r.Id.ToString().Contains(request.Keyword));

            }
            if (request.FormatId != null)
            {
                query = query.Where(x => x.r.FormatId == request.FormatId);
            }
            PageResult<RoomMD> result = new PageResult<RoomMD>();
            result.TotalRecord = await query.CountAsync();
            result.PageIndex = request.PageIndex;
            result.PageSize = request.PageSize;

            var rooms = query.Select(x => new RoomMD()
            {
                Id = x.r.Id,
                Name = x.r.Name,

            }).OrderBy(x => x.Id).Skip((request.PageIndex - 1) * (request.PageSize)).Take(request.PageSize).ToList();
            result.Item = rooms;

            return result;


        }

        public Task<List<SeatVMD>> GetSeatsInRoom(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<RoomMD>> GetRoomById(int id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return new ApiErrorResult<RoomMD>("Không tìm thấy phòng");
            }
            else
            {
                var result = new RoomMD()
                {
                    Id = room.Id,
                    Name = room.Name,
                    FormatId = room.FormatId
                };
                return new ApiSuccessResult<RoomMD>(result);
            }
        }

        public async Task<ApiResult<List<RoomVMD>>> GetAllRoomAsync()
        {
            var query = from r in _context.Rooms
                        join f in _context.RoomFormats on r.FormatId equals f.Id
                        select new { r, f };


            var rooms =await query.Select(x => new RoomVMD()
            {
                Id = x.r.Id,
                Name = x.r.Name,
                Format = x.f.Name 

            }).OrderBy(x => x.Id).ToListAsync();


            return new ApiSuccessResult<List<RoomVMD>>(rooms);
        }






        // RoomFormat 


    }
}