using Microsoft.EntityFrameworkCore;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTheater.Data.Models;

namespace MovieTheater.Application.FilmServices.Actors
{
    public class ActorService : IActorService
    {
        private MoviesContext _context;

        public ActorService(MoviesContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateAsync(ActorCreateRequest request)
        {
            Actor actor = new Actor()
            {
                Name = request.Name,
                Description = request.Description,
                Dob = request.DOB
            };
            _context.Actors.Add(actor);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Thêm thất bại");
            }

            return new ApiSuccessResult<bool>(true, "Thêm thành công");
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            Actor Actor = await _context.Actors.FindAsync(id);
            if (Actor == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                if (_context.Joinings.Count(x => x.ActorId == id) != 0)
                    return new ApiErrorResult<bool>("Không thể xóa nghệ sĩ");

                _context.Actors.Remove(Actor);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResult<bool>(true, "Xóa thành công");
                }
                else return new ApiErrorResult<bool>("Không xóa được");
            }
        }

        public async Task<ApiResult<bool>> UpdateAsync(ActorUpdateRequest request)
        {
            Actor actor = await _context.Actors.FindAsync(request.Id);
            if (actor == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                actor.Id = request.Id;
                actor.Dob = request.DOB;
                actor.Description = request.Description;
                actor.Name = request.Name;
                _context.Update(actor);
                int rs = await _context.SaveChangesAsync();
                if (rs == 0)
                {
                    return new ApiErrorResult<bool>("Cập nhật thất bại");
                }

                return new ApiSuccessResult<bool>(true, "Cập nhật thành công");
            }
        }

        public async Task<ApiResult<PageResult<ActorVMD>>> GetActorPagingAsync(ActorPagingRequest request)
        {
            var Actors = _context.Actors.Select(x => x);
            if (request.Keyword != null)
            {
                Actors = Actors.Where(x => x.Id.ToString().Contains(request.Keyword) ||
                                           x.Name.Contains(request.Keyword) ||
                                           x.Dob.ToString().Contains(request.Keyword));
            }

            int totalRow = await Actors.CountAsync();
            var item = Actors.OrderBy(x => x.Name).Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new ActorVMD()
                {
                    Id = x.Id,
                    Name = x.Name,
                    DOB = x.Dob,
                    Description = x.Description
                }).ToList();

            var pageResult = new PageResult<ActorVMD>()
            {
                TotalRecord = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Item = item,
            };

            return new ApiSuccessResult<PageResult<ActorVMD>>(pageResult);
        }

        public async Task<ApiResult<List<ActorVMD>>> GetAllAsync()
        {
            var Actors = await _context.Actors.Select(x => new ActorVMD()
            {
                Description = x.Description,
                DOB = x.Dob,
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return new ApiSuccessResult<List<ActorVMD>>(Actors);
        }

        public async Task<ApiResult<ActorVMD>> GetById(int id)
        {
            Actor actor = await _context.Actors.FindAsync(id);
            if (actor == null)
            {
                return new ApiErrorResult<ActorVMD>("Không tìm thấy");
            }
            else
            {
                var result = new ActorVMD()
                {
                    Id = actor.Id,
                    Name = actor.Name,
                    Description = actor.Description,
                    DOB = actor.Dob
                };
                return new ApiSuccessResult<ActorVMD>(result);
            }
        }
    }
}