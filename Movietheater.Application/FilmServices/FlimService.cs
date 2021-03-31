using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.FilmServices
{
    public class FlimService : IFilmService
    {
        private readonly MovieTheaterDBContext _context;
        public FlimService(MovieTheaterDBContext context)
        {
            _context = context;
        }


        public async Task<ApiResultLite> CreateAsync(FilmCreateRequest model)
        {
            var room = new Film()
            {
                Name = model.Name,
                Description = model.Description,
                BanId = model.BanId,
                Length = model.Length,
                PublishDate = model.PublishDate,
                TrailerURL = model.TrailerURL
            };

            await _context.AddAsync(room);
            if (await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResultLite("Không thể thêm phòng");
            }
            return new ApiSuccessResultLite("Thêm thành công");

        }
        public async Task<ApiResultLite> UpdateAsync(FilmUpdateRequest model)
        {
            Film film = await _context.Films.FindAsync(model.Id);
            if (film == null)
            {
                return new ApiErrorResultLite("Không tìm thấy phòng");
            }
            else
            {
                film.Name = model.Name;
                film.Description = model.Description;
                film.BanId = model.BanId;
                film.Length = model.Length;
                film.PublishDate = model.PublishDate;
                film.TrailerURL = model.TrailerURL;
                _context.Films.Update(film);
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
            Film film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return new ApiErrorResultLite("Không tìm thấy phim");
            }
            else
            {
                _context.Films.Remove(film);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResultLite("Xóa thành công");
                }
                else return new ApiSuccessResultLite("Không xóa được");
            }
        }

        public Task<PageResult<FilmVMD>> GetFilmPagingAsync(FilmPagingRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<FilmVMD>> GetFilmById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
