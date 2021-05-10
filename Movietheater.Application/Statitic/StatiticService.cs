using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.EF;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.ChartTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.Statitic
{
    public class StatiticService :IStatiticService
    {
        private readonly MovieTheaterDBContext _context;
        public StatiticService(MovieTheaterDBContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<ChartData>> GetTopGrossingFilmAsync(CalRevenueRequest request)
        {
            var query = from s in _context.Screenings
                        join f in _context.Films on s.FilmId equals f.Id
                        join t in _context.Tickets on s.Id equals t.ScreeningId
                        where s.StartTime.Date >= request.StartDate.Date && s.StartTime.Date <= request.EndDate.Date
                        select new { s,f,t};

            var grossing = await query.GroupBy(x => new { x.f.Name,x.f.Id}).Select(x => new 
                {
                    Name = x.Key.Name,
                    Grossing = (decimal)x.Sum(sft => sft.t.Price)
                }).OrderByDescending(x => x.Grossing).ToListAsync();

            ChartData chartData = new ChartData();
            chartData.Lables = grossing.Select(x => x.Name).ToList();
            chartData.DataRows[0] = grossing.Select(x => x.Grossing).ToList();

            return new ApiSuccessResult<ChartData>(chartData);
                        

        }
        public async Task<ApiResult<long>> GetRevenueAsync(CalRevenueRequest request)
        {
            var query = from s in _context.Screenings
                        join t in _context.Tickets on s.Id equals t.ScreeningId
                        where s.StartTime.Date > request.StartDate && s.StartTime.Date < request.EndDate
                        select t;
            long revenue = query.Sum(x => x.Price);
            return new ApiSuccessResult<long>(revenue);
        }
        public async Task<ApiResult<ChartData>> GetGroosingTypeAsync(CalRevenueRequest request)
        {
            var query = from s in _context.Screenings
                        join t in _context.Tickets on s.Id equals t.ScreeningId
                        join r in _context.Reservations on t.ReservationId  equals r.Id
                        join rt in _context.ReservationTypes on r.ReservationTypeId equals rt.Id
                        where s.StartTime.Date > request.StartDate && s.StartTime.Date < request.EndDate
                        select new { rt, t };
            
            var grossing = await query.GroupBy(x => new { x.rt.Name }).Select(x => new
            {
                Name = x.Key.Name,
                Grossing = (decimal)x.Sum(sft => sft.t.Price)
            }).ToListAsync();

            ChartData chartData = new ChartData();
            chartData.Lables = grossing.Select(x => x.Name).ToList();
            chartData.DataRows[0] = grossing.Select(x => x.Grossing).ToList();

            return new ApiSuccessResult<ChartData>(chartData);

        }
    }
}
