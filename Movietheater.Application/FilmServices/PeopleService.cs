using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movietheater.Application.FilmServices
{
    public class PeopleService : IPeopleService
    {
        private MovieTheaterDBContext _context;

        public PeopleService(MovieTheaterDBContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateAsync(PeopleCreateRequest request)
        {
            People people = new People()
            {
                Name = request.Name,
                Description = request.Description,
                DOB = request.DOB
            };
            _context.Peoples.Add(people);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Thêm thất bại");
            }

            return new ApiSuccessResult<bool>(true,"Thêm thành công");
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            People people = await _context.Peoples.FindAsync(id);
            if (people == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                if (_context.Joinings.Where(x => x.PeppleId == id).Count() != 0)
                    return new ApiErrorResult<bool>("Không thể xóa nghệ sĩ");

                _context.Peoples.Remove(people);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResult<bool>(true,"Xóa thành công");
                }
                else return new ApiErrorResult<bool>("Không xóa được");
            }
        }

        public async Task<ApiResult<bool>> UpdateAsync(PeopleUpdateRequest request)
        {
            People people = await _context.Peoples.FindAsync(request.Id);
            if (people == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                people.Id = request.Id;
                people.DOB = request.DOB;
                people.Description = request.Description;
                people.Name = request.Name;
                _context.Update(people);
                int rs = await _context.SaveChangesAsync();
                if (rs == 0)
                {
                    return new ApiErrorResult<bool>("Cập nhật thất bại");
                }
                return new ApiSuccessResult<bool>(true,"Cập nhật thành công");
            }
        }

        public async Task<ApiResult<PageResult<PeopleVMD>>> GetPeoplePagingAsync(PeoplePagingRequest request)
        {
            var peoples = _context.Peoples.Select(x => x);
            if (request.Keyword != null)
            {
                peoples = peoples.Where(x => x.Id.ToString().Contains(request.Keyword) ||
                                                x.Name.Contains(request.Keyword) ||
                                                x.DOB.ToString().Contains(request.Keyword));
            }
            int totalRow = await peoples.CountAsync();
            var item = peoples.OrderBy(x => x.Name).Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new PeopleVMD()
                {
                    Id = x.Id,
                    Name = x.Name,
                    DOB = x.DOB,
                    Description = x.Description
                }).ToList();

            var pageResult = new PageResult<PeopleVMD>()
            {
                TotalRecord = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Item = item,
            };

            return new ApiSuccessResult<PageResult<PeopleVMD>>(pageResult);
        }

        public async Task<ApiResult<List<PeopleVMD>>> GetAllPeopleAsync()
        {
            var peoples = await _context.Peoples.Select(x => new PeopleVMD()
            {
                Description = x.Description,
                DOB = x.DOB,
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return new ApiSuccessResult<List<PeopleVMD>>(peoples);
        }

        public async Task<ApiResult<PeopleVMD>> GetPeopleById(int id)
        {
            People people = await _context.Peoples.FindAsync(id);
            if (people == null)
            {
                return new ApiErrorResult<PeopleVMD>("Không tìm thấy");
            }
            else
            {
                var result = new PeopleVMD()
                {
                    Id = people.Id,
                    Name = people.Name,
                    Description = people.Description,
                    DOB = people.DOB
                };
                return new ApiSuccessResult<PeopleVMD>(result);
            }
        }
    }
}