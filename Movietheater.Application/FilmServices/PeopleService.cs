using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<ApiResultLite> CreateAsync(string name)
        {
            People people = new People()
            {
                Name = name
            };
            _context.Peoples.Add(people);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResultLite("Thêm thất bại");
            }

            return new ApiSuccessResultLite("Thêm thành công");
        }

        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            People people = await _context.Peoples.FindAsync(id);
            if (people == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else
            {
                _context.Peoples.Remove(people);
                await _context.SaveChangesAsync();
                return new ApiSuccessResultLite("Xóa thành công");
            }
        }

        public async Task<ApiResultLite> UpdateAsync(PeopleUpdateRequest request)
        {
            People people = await _context.Peoples.FindAsync(request.Id);
            if (people == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
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
                    return new ApiErrorResultLite("Cập nhật thất bại");
                }
                return new ApiSuccessResultLite("Cập nhật thành công");
            }
        }
    }
}
