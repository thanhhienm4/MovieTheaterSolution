using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Movietheater.Application.Common;
using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.FilmServices
{
    public class FlimService : IFilmService
    {
        private readonly MovieTheaterDBContext _context;
        private readonly IStorageService _storageService;
        private readonly IConfiguration _configuration;
        public FlimService(MovieTheaterDBContext context, IStorageService storageService, IConfiguration configuration)
        {

            _context = context;
            _storageService = storageService;
            _configuration = configuration;
        }


        public async Task<ApiResultLite> CreateAsync(FilmCreateRequest request)
        {
            string posterPath = await SaveFile(request.Poster);
            var room = new Film()
            {
                Name = request.Name,
                Description = request.Description,
                BanId = request.BanId,
                Length = request.Length,
                PublishDate = request.PublishDate,
                TrailerURL = request.TrailerURL,
                Poster = posterPath
            };

            await _context.AddAsync(room);
            if (await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResultLite("Không thể thêm phòng");
            }
            return new ApiSuccessResultLite("Thêm thành công");

        }
        public async Task<ApiResultLite> UpdateAsync(FilmUpdateRequest request)
        {
            Film film = await _context.Films.FindAsync(request.Id);
            if (film == null)
            {
                return new ApiErrorResultLite("Không tìm thấy phòng");
            }
            else
            {
                film.Name = request.Name;
                film.Description = request.Description;
                film.BanId = request.BanId;
                film.Length = request.Length;
                film.PublishDate = request.PublishDate;
                film.TrailerURL = request.TrailerURL;

                if(request.Poster != null)
                {   
                    try
                    {
                        await _storageService.DeleteFileAsync(film.Poster);
                    }catch(Exception e)
                    {

                    }              
                    string newPosterPath = await this.SaveFile(request.Poster);
                    film.Poster = newPosterPath;
                }
                
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
                string poster = film.Poster;
                _context.Films.Remove(film);
                if (await _context.SaveChangesAsync() != 0)
                {
                    await _storageService.DeleteFileAsync(poster);
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
                    PublishDate = film.PublishDate,
                    TrailerURL = film.TrailerURL,
                    Poster = $"{_configuration["BackEndServer"]}/" +
                    $"{FileStorageService.USER_CONTENT_FOLDER_NAME}/{film.Poster}"

                };
                return new ApiSuccessResult<FilmVMD>(result);
            }
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            string originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
