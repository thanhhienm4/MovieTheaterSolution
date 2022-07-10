using Microsoft.EntityFrameworkCore;
using MovieTheater.Application.FilmServices;
using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Application.ScreeningServices
{
    public class ScreeningService : IScreeningService
    {
        private readonly MovieTheaterDBContext _context;
        private readonly IFilmService _filmService;

        public ScreeningService(MovieTheaterDBContext context, IFilmService filmService)
        {
            _context = context;
            _filmService = filmService;
        }

        public async Task<ApiResult<bool>> CreateAsync(ScreeningCreateRequest request)
        {
            DateTime publishDate = _context.Films.Where(x => x.Id == request.FilmId).Select(x => x.PublishDate).FirstOrDefault();
            if (publishDate.Date > request.StartTime.Date)
                return new ApiErrorResult<bool>("Thời gian chiếu không được trước ngày công chiếu " + publishDate.ToString("dd/MM/yyyy"));

            Screening screening = new Screening()
            {
                StartTime = request.StartTime,
                // Surcharge = request.Surcharge,
                FilmId = request.FilmId,
                RoomId = request.RoomId,
                KindOfScreeningId = request.KindOfScreeningId,
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
                return new ApiSuccessResult<bool>(true,"Thêm thành công");
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
                    if (screening.Tickets != null)
                    {
                        screening.Active = false;
                        _context.Screenings.Update(screening);
                    }
                    else
                    {
                        _context.Screenings.Remove(screening);
                    }

                    await _context.SaveChangesAsync();
                    return new ApiSuccessResult<bool>(true,"Xóa thành công");
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
                        join f in _context.Films on s.FilmId equals f.Id
                        join r in _context.Rooms on s.RoomId equals r.Id
                        join kos in _context.KindOfScreenings on s.KindOfScreeningId equals kos.Id
                        select new { s, f, r, kos };

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
                Film = x.f.Name,
                Room = x.r.Name,
                StartTime = x.s.StartTime,
                FinishTime = x.s.StartTime.AddMinutes(x.f.Length),
                KindOfScreening = x.kos.Name
            }).OrderByDescending(x => x.StartTime).Skip((request.PageIndex - 1) * (request.PageSize)).Take(request.PageSize).ToList();
            result.Item = rooms;

            return new ApiSuccessResult<PageResult<ScreeningVMD>>(result);
        }

        public Task<PageResult<FilmScreeningVMD>> GetScreeningTimePagingAsync(ScreeningPagingRequest request)
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
                DateTime publishDate = _context.Films.Where(x => x.Id == request.FilmId).Select(x => x.PublishDate).FirstOrDefault();
                if (publishDate.Date > request.StartTime.Date)
                    return new ApiErrorResult<bool>("Thời gian chiếu không được trước ngày công chiếu " + publishDate.ToString("dd/MM/yyyy"));
                if(screening.StartTime <= DateTime.Now)
                {
                    return new ApiErrorResult<bool>("Không thể cập nhật xuất chiếu đó xuất chiếu đã diễn ra");
                }    
                screening.Id = request.Id;
                screening.StartTime = request.StartTime;

                screening.FilmId = request.FilmId;
                screening.RoomId = request.RoomId;
                screening.KindOfScreeningId = request.KindOfScreeningId;

                _context.Update(screening);

                try
                {
                    int rs = await _context.SaveChangesAsync();
                    if (rs == 0)
                    {
                        return new ApiErrorResult<bool>("Cập nhật thất bại");
                    }
                    return new ApiSuccessResult<bool>(true,"Cập nhật thành công");
                }
                catch (DbUpdateException)
                {
                    return new ApiErrorResult<bool>("Cập nhật thất bại");
                }
            }
        }

        public async Task<ApiResult<ScreeningMD>> GetScreeningMDByIdAsync(int id)
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
                    FilmId = screening.FilmId,
                    RoomId = screening.RoomId,
                    StartTime = screening.StartTime,
                    KindOfScreeningId = screening.KindOfScreeningId
                };
                return new ApiSuccessResult<ScreeningMD>(screeningVMD);
            }
        }

        public async Task<ApiResult<ScreeningVMD>> GetScreeningVMDByIdAsync(int id)
        {
            var screening = await _context.Screenings.FindAsync(id);
            if (screening == null)
            {
                return new ApiErrorResult<ScreeningVMD>("Không tìm thấy xuất chiếu");
            }
            else
            {
                var query = from s in _context.Screenings
                            join f in _context.Films on s.FilmId equals f.Id
                            join r in _context.Rooms on s.RoomId equals r.Id
                            join kos in _context.KindOfScreenings on s.KindOfScreeningId equals kos.Id
                            select new { s, f, r, kos };

                var screeningVMD = await query.Select(x => new ScreeningVMD()
                {
                    Id = x.s.Id,
                    Film = x.f.Name,
                    Room = x.r.Name,
                    StartTime = x.s.StartTime,
                    KindOfScreening = x.kos.Name
                }).FirstOrDefaultAsync();
                return new ApiSuccessResult<ScreeningVMD>(screeningVMD);
            }
        }

        public async Task<ApiResult<List<FilmScreeningVMD>>> GetFilmScreeningInday(DateTime? date)

        {
            if (date == null)
                date = DateTime.Now;

            var screenings = await _context.Screenings.Where(x => x.StartTime.Date == date.GetValueOrDefault().Date).
                                                Select(x => new ScreeningMD()
                                                {
                                                    Id = x.Id,
                                                    StartTime = x.StartTime,
                                                    FilmId = x.FilmId,
                                                    RoomId = x.RoomId
                                                }).ToListAsync();

            List<FilmScreeningVMD> filmScreenings = new List<FilmScreeningVMD>();
            Dictionary<int, List<ScreeningMD>> dic = new Dictionary<int, List<ScreeningMD>>();

            foreach (var screening in screenings)
            {
                if (!dic.ContainsKey(screening.FilmId))
                {
                    dic.Add(screening.FilmId, new List<ScreeningMD>());
                }
                dic[screening.FilmId].Add(screening);
            }

            foreach (var pair in dic)
            {
                filmScreenings.Add(new FilmScreeningVMD()
                {
                    Film = (await _filmService.GetFilmVMDById(pair.Key)).ResultObj,
                    ListScreening = pair.Value,
                });
            }
            return new ApiSuccessResult<List<FilmScreeningVMD>>(filmScreenings);
        }

        public async Task<ApiResult<ScreeningOfFilmInWeekVMD>> GetListCreeningOfFilmInWeek(int filmId)
        {
            var screenings = await _context.Screenings.Where(x => x.StartTime.Date >= DateTime.Now.Date &&
                                                    x.StartTime <= DateTime.Now.AddDays(6).Date &&
                                                    x.FilmId == filmId).
                                               Select(x => new ScreeningMD()
                                               {
                                                   Id = x.Id,
                                                   StartTime = x.StartTime,
                                                   FilmId = x.FilmId,
                                                   RoomId = x.RoomId
                                               }).ToListAsync();

            ScreeningOfFilmInWeekVMD sof = new ScreeningOfFilmInWeekVMD();
            sof.Film = (await _filmService.GetFilmVMDById(filmId)).ResultObj;
            sof.Screenings = new List<List<ScreeningMD>>();

            for (int i = 0; i <= 6; i++)
            {
                var listScrening = screenings.Where(x => x.StartTime.Date == DateTime.Now.AddDays(i).Date).ToList();
                sof.Screenings.Add(listScrening);
            }

            return new ApiSuccessResult<ScreeningOfFilmInWeekVMD>(sof);
        }
    }
}