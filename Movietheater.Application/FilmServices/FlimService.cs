using Microsoft.EntityFrameworkCore;
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

        public async Task<ApiResult<PageResult<FilmVMD>>> GetFilmPagingAsync(FilmPagingRequest request)
        {
            var films = _context.Films.Select(x => x);

            if (!string.IsNullOrWhiteSpace(request.Keyword))
                films = films.Where(x => x.Name.Contains(request.Keyword)
                                        || x.Id.ToString().Contains(request.Keyword));


            int totalRow = await films.CountAsync();
            var item = films.OrderBy(x => x.Name).Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new FilmVMD()
                {
                    Id = x.Id,
                    Name = x.Name,
                    PublishDate = x.PublishDate,
                    BanId = x.BanId
                }).ToList();

            var pageResult = new PageResult<FilmVMD>()
            {

                TotalRecord = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Item = item,
            };

            return new ApiSuccessResult<PageResult<FilmVMD>>(pageResult);
        }

        public async Task<ApiResult<FilmVMD>> GetFilmById(int id)
        {
            Film film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return new ApiErrorResult<FilmVMD>("Không tìm thấy phim");
            }
            else
            {
                var result = new FilmVMD()
                {
                    Id = film.Id,
                    Name = film.Name,
                    BanId = film.BanId,
                    Description = film.Description,
                    Length = film.Length,
                    Poster = film.Poster,
                    PublishDate = film.PublishDate,
                    TrailerURL = film.TrailerURL

                };
                return new ApiSuccessResult<FilmVMD>(result);
            }
        }
    }
}
