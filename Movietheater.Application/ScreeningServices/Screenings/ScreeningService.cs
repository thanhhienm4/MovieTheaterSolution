using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Application.FilmServices.Movies;
using MovieTheater.Data.Models;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Application.ScreeningServices.Screenings
{
    public class ScreeningService : IScreeningService
    {
        private readonly MoviesContext _context;
        private readonly IMovieService _filmService;

        public ScreeningService(MoviesContext context, IMovieService filmService)
        {
            _context = context;
            _filmService = filmService;
        }

        public async Task<ApiResult<bool>> CreateAsync(ScreeningCreateRequest request)
        {
            DateTime publishDate = _context.Movies.Where(x => x.Id == request.FilmId).Select(x => x.PublishDate)
                .FirstOrDefault();
            if (publishDate.Date > request.StartTime.Date)
                return new ApiErrorResult<bool>("Thời gian chiếu không được trước ngày công chiếu " +
                                                publishDate.ToString("dd/MM/yyyy"));

            if (!CheckTime(request.FilmId, request.AuditoriumId, request.StartTime))
                return new ApiErrorResult<bool>("Vui lòng kiểm tra lại thời gian bắt đầu");

            Screening screening = new Screening()
            {
                StartTime = request.StartTime,
                MovieId = request.FilmId,
                AuditoriumId = request.AuditoriumId,
                Active = true
            };

            _context.Screenings.Add(screening);
            try
            {
                int result = await _context.SaveChangesAsync();
                if (result == 0)
                {
                    return new ApiErrorResult<bool>("Thêm thất bại");
                }

                return new ApiSuccessResult<bool>(true, "Thêm thành công");
            }
            catch (DbUpdateException)
            {
                return new ApiErrorResult<bool>("Thêm thất bại vui lòng kiểm tra lại thời gian bắt đầu");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            Screening screening = await _context.Screenings.FindAsync(id);
            if (screening == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else

            {
                try
                {
                    if (screening.Reservations != null)
                    {
                        screening.Active = false;
                        _context.Screenings.Update(screening);
                    }
                    else
                    {
                        _context.Screenings.Remove(screening);
                    }

                    await _context.SaveChangesAsync();
                    return new ApiSuccessResult<bool>(true, "Xóa thành công");
                }
                catch (DbUpdateException)
                {
                    return new ApiErrorResult<bool>("Xóa thất bại do suất chiếu đã được khách mua vé");
                }
            }
        }

        public async Task<ApiResult<PageResult<ScreeningVMD>>> GetScreeningPagingAsync(ScreeningPagingRequest request)
        {
            var query = from s in _context.Screenings
                        join f in _context.Movies on s.MovieId equals f.Id
                        join r in _context.Auditoriums on s.AuditoriumId equals r.Id
                        select new { s, f, r };

            if (request.Keyword != null)
            {
                query = query.Where(x => x.s.Id.ToString().Contains(request.Keyword) ||
                                         x.s.StartTime.ToString().Contains(request.Keyword) ||
                                         x.f.Name.Contains(request.Keyword));
            }

            if (request.Date != null)
                query = query.Where(x => x.s.StartTime.Date == request.Date);

            PageResult<ScreeningVMD> result = new PageResult<ScreeningVMD>();
            result.TotalRecord = await query.CountAsync();
            result.PageIndex = request.PageIndex;
            result.PageSize = request.PageSize;

            var rooms = query.Select(x => new ScreeningVMD()
            {
                Id = x.s.Id,
                Movie = x.f.Name,
                Auditorium = x.r.Name,
                StartTime = x.s.StartTime,
                FinishTime = x.s.StartTime.AddMinutes(x.f.Length),
            }).OrderByDescending(x => x.StartTime).Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
            result.Item = rooms;

            return new ApiSuccessResult<PageResult<ScreeningVMD>>(result);
        }

        public Task<PageResult<MovieScreeningVMD>> GetScreeningTimePagingAsync(ScreeningPagingRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<bool>> UpdateAsync(ScreeningUpdateRequest request)
        {
            Screening screening = await _context.Screenings.FindAsync(request.Id);
            if (screening == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                DateTime publishDate = _context.Movies.Where(x => x.Id == request.FilmId).Select(x => x.PublishDate)
                    .FirstOrDefault();
                if (publishDate.Date > request.StartTime.Date)
                    return new ApiErrorResult<bool>("Thời gian chiếu không được trước ngày công chiếu " +
                                                    publishDate.ToString("dd/MM/yyyy"));
                if (screening.StartTime <= DateTime.Now)
                {
                    return new ApiErrorResult<bool>("Không thể cập nhật xuất chiếu đó xuất chiếu đã diễn ra");
                }

                screening.Id = request.Id;
                screening.StartTime = request.StartTime;

                screening.MovieId = request.FilmId;
                screening.AuditoriumId = request.AuditoriumId;

                _context.Update(screening);

                try
                {
                    int rs = await _context.SaveChangesAsync();
                    if (rs == 0)
                    {
                        return new ApiErrorResult<bool>("Cập nhật thất bại");
                    }

                    return new ApiSuccessResult<bool>(true, "Cập nhật thành công");
                }
                catch (DbUpdateException)
                {
                    return new ApiErrorResult<bool>("Cập nhật thất bại");
                }
            }
        }

        public async Task<ApiResult<ScreeningMD>> GetMDByIdAsync(int id)
        {
            var screening = await _context.Screenings.FindAsync(id);
            if (screening == null)
            {
                return new ApiErrorResult<ScreeningMD>("Không tìm thấy xuất chiếu");
            }
            else
            {
                var screeningVMD = new ScreeningMD()
                {
                    Id = screening.Id,
                    MovieId = screening.MovieId,
                    AuditoriumId = screening.AuditoriumId,
                    StartTime = screening.StartTime,
                };
                return new ApiSuccessResult<ScreeningMD>(screeningVMD);
            }
        }

        public async Task<ApiResult<ScreeningVMD>> GetVMDByIdAsync(int id)
        {
            var screening = await _context.Screenings.FindAsync(id);
            if (screening == null)
            {
                return new ApiErrorResult<ScreeningVMD>("Không tìm thấy xuất chiếu");
            }
            else
            {
                var query = from s in _context.Screenings
                            join f in _context.Movies on s.MovieId equals f.Id
                            join r in _context.Auditoriums on s.AuditoriumId equals r.Id
                            select new { s, f, r };
                var screeningVMD = await query.Select(x => new ScreeningVMD()
                {
                    Id = x.s.Id,
                    Movie = x.f.Name,
                    Auditorium = x.r.Name,
                    StartTime = x.s.StartTime,
                }).FirstOrDefaultAsync();
                return new ApiSuccessResult<ScreeningVMD>(screeningVMD);
            }
        }

        public async Task<ApiResult<List<MovieScreeningVMD>>> GetFilmScreeningInday(DateTime? date)

        {
            if (date == null)
                date = DateTime.Now;

            var screenings = await _context.Screenings.Where(x => x.StartTime.Date == date.GetValueOrDefault().Date)
                .Select(x => new ScreeningMD()
                {
                    Id = x.Id,
                    StartTime = x.StartTime,
                    MovieId = x.MovieId,
                    AuditoriumId = x.AuditoriumId
                }).ToListAsync();

            List<MovieScreeningVMD> filmScreenings = new List<MovieScreeningVMD>();
            Dictionary<string, List<ScreeningMD>> dic = new Dictionary<string, List<ScreeningMD>>();

            foreach (var screening in screenings)
            {
                if (!dic.ContainsKey(screening.MovieId))
                {
                    dic.Add(screening.MovieId, new List<ScreeningMD>());
                }

                dic[screening.MovieId].Add(screening);
            }

            foreach (var pair in dic)
            {
                filmScreenings.Add(new MovieScreeningVMD()
                {
                    Movie = (await _filmService.GetFilmVMDById(pair.Key)).ResultObj,
                    ListScreening = pair.Value,
                });
            }

            return new ApiSuccessResult<List<MovieScreeningVMD>>(filmScreenings);
        }

        public async Task<ApiResult<ScreeningOfFilmInWeekVMD>> GetListOfMovieInWeek(string movieId)
        {
            var screenings = await _context.Screenings.Where(x => x.StartTime.Date >= DateTime.Now.Date &&
                                                                  x.StartTime <= DateTime.Now.AddDays(6).Date &&
                                                                  x.MovieId == movieId).Select(x => new ScreeningMD()
                                                                  {
                                                                      Id = x.Id,
                                                                      StartTime = x.StartTime,
                                                                      MovieId = x.MovieId,
                                                                      AuditoriumId = x.MovieId
                                                                  }).ToListAsync();

            ScreeningOfFilmInWeekVMD sof = new ScreeningOfFilmInWeekVMD();
            sof.Movie = (await _filmService.GetFilmVMDById(movieId)).ResultObj;
            sof.Screenings = new List<List<ScreeningMD>>();

            for (int i = 0; i <= 6; i++)
            {
                var listScrening = screenings.Where(x => x.StartTime.Date == DateTime.Now.AddDays(i).Date).ToList();
                sof.Screenings.Add(listScrening);
            }

            return new ApiSuccessResult<ScreeningOfFilmInWeekVMD>(sof);
        }

        public bool CheckTime(string movieId, string auditoriumId, DateTime time)
        {
            var parameterReturn = new SqlParameter
            {
                ParameterName = "@ReturnValue",
                SqlDbType = System.Data.SqlDbType.Bit,
                Direction = System.Data.ParameterDirection.Output,
            };
            var startTime = new SqlParameter("@startTime", time);
            var movie = new SqlParameter("@movieId", movieId);
            var auditorium = new SqlParameter("@auditoriumId", auditoriumId);

            var data = _context.Database.ExecuteSqlRaw("set @ReturnValue = dbo.[Function_CheckTime] ( @startTime , @movieId , @auditoriumId)", parameterReturn, startTime, movie, auditorium);

            return (bool)parameterReturn.Value;
        }
    }
}