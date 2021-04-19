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
        public async Task<ApiResult<List<FilmVMD>>> GetAllFilmAsync()
        {
            var query = from f in _context.Films
                        join b in _context.Bans on f.BanId equals b.Id
                        select new { f, b };

            var films = await query.Select(x => new FilmVMD()
                {
                    Id = x.f.Id,
                    Name = x.f.Name,
                    PublishDate = x.f.PublishDate,
                    Ban = x.b.Name,
                    Poster = $"{_configuration["BackEndServer"]}/" +
                   $"{FileStorageService.USER_CONTENT_FOLDER_NAME}/{x.f.Poster}",
                    Description = x.f.Description,
                    TrailerURL = x.f.TrailerURL

                }).ToListAsync();

            return new ApiSuccessResult<List<FilmVMD>>(films);
        }
        public async Task<ApiResult<PageResult<FilmVMD>>> GetFilmPagingAsync(FilmPagingRequest request)
        {
            var query = from f in _context.Films 
                        join b in _context.Bans on f.BanId equals b.Id 
                        select new {f,b };

            if (!string.IsNullOrWhiteSpace(request.Keyword))
                query = query.Where(x => x.f.Name.Contains(request.Keyword)
                                        || x.f.Id.ToString().Contains(request.Keyword)
                                        || x.b.Name.Contains(request.Keyword));


            int totalRow = await query.CountAsync();
            var films = query.OrderBy(x => x.f.Name).Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select( x => new FilmVMD()
                {
                    Id = x.f.Id,
                    Name = x.f.Name,
                    PublishDate = x.f.PublishDate,
                    Ban = x.b.Name,
                    Poster = $"{_configuration["BackEndServer"]}/" +
                    $"{FileStorageService.USER_CONTENT_FOLDER_NAME}/{x.f.Poster}",
                    Description = x.f.Description,
                    TrailerURL = x.f.TrailerURL

                }).ToList();

            if(films != null)
            {
                foreach(var film in films)
                {
                    film.Genres = GetGenres(film.Id);
                }
            }

            var pageResult = new PageResult<FilmVMD>()
            {

                TotalRecord = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Item = films,
            };

            return new ApiSuccessResult<PageResult<FilmVMD>>(pageResult);
        }

        public async Task<ApiResult<FilmMD>> GetFilmMDById(int id)
        {
            Film film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return new ApiErrorResult<FilmMD>("Không tìm thấy phim");
            }
            else
            {
                var result = new FilmMD()
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
                return new ApiSuccessResult<FilmMD>(result);
            }
        }
        public async Task<ApiResult<FilmVMD>> GetFilmVMDById(int id)
        {
            Film film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return new ApiErrorResult<FilmVMD>("Không tìm thấy phim");
            }
            else
            {
                var query = from f in _context.Films
                            join b in _context.Bans on f.BanId equals b.Id
                            where f.Id == id
                            select new { f, b };

                var filmVMD = await query.Select(x => new FilmVMD()
                {
                    Id = x.f.Id,
                    Name = x.f.Name,
                    PublishDate = x.f.PublishDate,
                    Ban = x.b.Name,
                    Poster = $"{_configuration["BackEndServer"]}/" +
                    $"{FileStorageService.USER_CONTENT_FOLDER_NAME}/{x.f.Poster}",
                    Description  = x.f.Description,
                    TrailerURL = x.f.TrailerURL

                }).FirstOrDefaultAsync();
                filmVMD.Genres = GetGenres(filmVMD.Id);
                return new ApiSuccessResult<FilmVMD>(filmVMD);
            }
        }

        public async Task<ApiResultLite> GenreAssignAsync(GenreAssignRequest request)
        {
            var film = await _context.Films.FindAsync(request.FilmId);
            if (film == null)
            {
                return new ApiErrorResultLite("Phim không tồn tại");
            }

            // check genres available 
            List<int> genres = request.Genres.Select(x =>Int32.Parse(x.Id)).ToList();
            if (!CheckGenres(genres))
            {
                return new ApiErrorResultLite("Yêu cầu không hợp lệ");
            }

            var filmInGenres = _context.FilmInGenres.Where(X => X.FilmId == request.FilmId).Select(x=>x);
            _context.FilmInGenres.RemoveRange(filmInGenres);

            var activeGenres = request.Genres.Where(x => x.Selected == true).Select(x => new FilmInGenre()
            {
                FilmId = request.FilmId, 
                FilmGenreId = Int32.Parse(x.Id)
            }) ;
            _context.FilmInGenres.AddRange(activeGenres);

            await _context.SaveChangesAsync();

            return new ApiSuccessResultLite("Gán danh mục thành công");
           
            
        }
        private bool CheckGenres (List<int> genres)
        {
            var query =  genres.Join(_context.FilmGenre,
                x => x,
                fg => fg.Id,
                (x, fg) => x).ToList();

            HashSet<int> setGenres = new HashSet<int>(genres);
            if (setGenres.Count == query.Count)
                return true;
            else
                return false;


        }
        private async Task<string> SaveFile(IFormFile file)
        {
            string originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        private List<string> GetGenres(int id)
        {
            return   _context.FilmInGenres.Join(_context.FilmGenre,
                                                            fig => fig.FilmGenreId,
                                                            fg => fg.Id,
                                                            (fig, fg) => fg.Name).ToList();
            
        }
    }
}
