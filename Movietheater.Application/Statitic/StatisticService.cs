using Microsoft.EntityFrameworkCore;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.ChartTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTheater.Data.Models;

namespace MovieTheater.Application.Statitic
{
    public class StatisticService : IStatisticService
    {
        private readonly MoviesContext _context;

        public StatisticService(MoviesContext context)
        {
            _context = context;
        }

        //public async Task<ApiResult<ChartData>> GetTopRevenueFilmAsync(CalRevenueRequest request)
        //{
        //    var query = from s in _context.Screenings
        //                join f in _context.Films on s.FilmId equals f.Id
        //                join t in _context.Tickets on s.Id equals t.ScreeningId
        //                join r in _context.Reservations on t.ReservationId equals r.Id
        //                where s.StartTime.Date >= request.StartDate.Date && s.StartTime.Date <= request.EndDate.Date && s.Active == true && r.Active == true
        //                select new { s, f, t };

        //    var Revenue = await query.GroupBy(x => new { x.f.RowName, x.f.Id }).Select(x => new
        //    {
        //        RowName = x.Key.RowName,
        //        Revenue = (decimal)x.Sum(sft => sft.t.Price)
        //    }).OrderByDescending(x => x.Revenue).ToListAsync();

        //    ChartData chartData = new ChartData();
        //    chartData.Lables = Revenue.Select(x => x.RowName).ToList();
        //    chartData.DataRows[0] = Revenue.Select(x => x.Revenue).ToList();

        //    return new ApiSuccessResult<ChartData>(chartData);
        //}

        //public async Task<ApiResult<long>> GetRevenueAsync(CalRevenueRequest request)
        //{
        //    var query = from s in _context.Screenings
        //                join t in _context.Tickets on s.Id equals t.ScreeningId
        //                join r in _context.Reservations on t.ReservationId equals r.Id
        //                where s.StartTime.Date > request.StartDate && s.StartTime.Date < request.EndDate
        //                && s.Active == true && r.Active == true
        //                select t;
        //    long revenue = await query.SumAsync(x => x.Price);
        //    return new ApiSuccessResult<long>(revenue);
        //}

        //public async Task<ApiResult<ChartData>> GetRevenueTypeAsync(CalRevenueRequest request)
        //{
        //    var query = from s in _context.Screenings
        //                join t in _context.Tickets on s.Id equals t.ScreeningId
        //                join r in _context.Reservations on t.ReservationId equals r.Id
        //                join rt in _context.ReservationTypes on r.ReservationTypeId equals rt.Id
        //                where s.StartTime.Date > request.StartDate && s.StartTime.Date < request.EndDate
        //                && s.Active == true && r.Active == true
        //                select new { rt, t };

        //    var Revenue = await query.GroupBy(x => new { x.rt.RowName }).Select(x => new
        //    {
        //        RowName = x.Key.RowName,
        //        Revenue = (decimal)x.Sum(sft => sft.t.Price)
        //    }).ToListAsync();

        //    ChartData chartData = new ChartData();
        //    chartData.Lables = Revenue.Select(x => x.RowName).ToList();
        //    chartData.DataRows[0] = Revenue.Select(x => x.Revenue).ToList();

        //    return new ApiSuccessResult<ChartData>(chartData);
        //}

        //public async Task<ApiResult<ChartData>> GetRevenueInNMonthAsync(int n)
        //{
        //    var temp = DateTime.Now.AddMonths(-n+1);
        //    DateTime startDate = GetDateStartMonth(temp);

        //    ChartData chartData = new ChartData();
        //    for (DateTime date = startDate;date <= DateTime.Now ;date = date.AddMonths(1))
        //    {
        //        chartData.Lables.Add(date.ToString("MM/yyyy"));
        //        chartData.DataRows[0].Add((await GetRevenueAsync( new CalRevenueRequest() 
        //        {
        //            StartDate = GetDateStartMonth(date),
        //            EndDate = GetDateEndMonth(date)
        //        } )).ResultObj);
        //    }


        //    return new ApiSuccessResult<ChartData>(chartData);
        //}
        //private DateTime GetDateEndMonth(DateTime date)
        //{
        //    var temp = date.AddMonths(1);
        //    var startNextMounth = new DateTime(temp.Year, temp.Month, 1);
        //    DateTime endDate = temp.AddDays(-1);
        //    return endDate;
        //}
        //private DateTime GetDateStartMonth(DateTime date)
        //{
        //    return new DateTime(date.Year, date.Month, 1);
        //}


        public Task<ApiResult<ChartData>> GetTopRevenueFilmAsync(CalRevenueRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<long>> GetRevenueAsync(CalRevenueRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<ChartData>> GetRevenueTypeAsync(CalRevenueRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<ChartData>> GetRevenueInNMonthAsync(int n)
        {
            throw new NotImplementedException();
        }
    }
}