﻿using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.Models;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.ChartTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Application.Statitic
{
    public class StatisticService : IStatisticService
    {
        private readonly MoviesContext _context;

        public StatisticService(MoviesContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<ChartData>> GetTopRevenueFilmAsync(CalRevenueRequest request)
        {
            var query = from m in _context.Movies
                        join s in _context.Screenings on m.Id equals s.MovieId
                        join r in _context.Reservations on s.Id equals r.ScreeningId into sr
                        from r in sr.DefaultIfEmpty()
                        join t in _context.Tickets on r.Id equals t.ReservationId
                        where r.Time.Date >= request.StartDate.Date && r.Time.Date <= request.EndDate.Date && s.Active == true && r.Active == true
                        select new { s, m, t };

            var Revenue = await query.GroupBy(x => new { x.m.Name, x.m.Id }).Select(x => new
            {
                Name = x.Key.Name,
                Revenue = (decimal)x.Sum(sft => sft.t.Price)
            }).OrderByDescending(x => x.Revenue).ToListAsync();

            ChartData chartData = new ChartData();
            chartData.Lables = Revenue.Select(x => x.Name).ToList();
            chartData.DataRows[0] = Revenue.Select(x => x.Revenue).ToList();
            return new ApiSuccessResult<ChartData>(chartData);
        }

        public async Task<ApiResult<long>> GetRevenueAsync(CalRevenueRequest request)
        {
            var query = from s in _context.Screenings
                        join r in _context.Reservations on s.Id equals r.ScreeningId
                        join t in _context.Tickets on r.Id equals t.ReservationId

                        where r.Time.Date >= request.StartDate.Date && r.Time.Date <= request.EndDate.Date && s.Active == true && r.Active == true
                        select t;

            long revenue = (long)await query.SumAsync(x => x.Price);
            return new ApiSuccessResult<long>(revenue);
        }

        public async Task<ApiResult<ChartData>> GetRevenueTypeAsync(CalRevenueRequest request)
        {
            var query = from s in _context.Screenings
                        join r in _context.Reservations on s.Id equals r.ScreeningId
                        join t in _context.Tickets on r.Id equals t.ReservationId
                        join rt in _context.ReservationTypes on r.TypeId equals rt.Id
                        where r.Time.Date > request.StartDate && r.Time.Date < request.EndDate
                        && s.Active == true && r.Active == true
                        select new { rt, t };

            var Revenue = await query.GroupBy(x => new { x.rt.Name }).Select(x => new
            {
                RowName = x.Key.Name,
                Revenue = (decimal)x.Sum(sft => sft.t.Price)
            }).ToListAsync();

            ChartData chartData = new ChartData();
            chartData.Lables = Revenue.Select(x => x.RowName).ToList();
            chartData.DataRows[0] = Revenue.Select(x => x.Revenue).ToList();

            return new ApiSuccessResult<ChartData>(chartData);
        }

        public async Task<ApiResult<ChartData>> GetRevenueInNMonthAsync(int n)
        {
            var temp = DateTime.Now.AddMonths(-n + 1);
            DateTime startDate = GetDateStartMonth(temp);

            ChartData chartData = new ChartData();
            for (DateTime date = startDate; date <= DateTime.Now; date = date.AddMonths(1))
            {
                chartData.Lables.Add(date.ToString("MM/yyyy"));
                chartData.DataRows[0].Add((await GetRevenueAsync(new CalRevenueRequest()
                {
                    StartDate = GetDateStartMonth(date),
                    EndDate = GetDateEndMonth(date)
                })).ResultObj);
            }
            return new ApiSuccessResult<ChartData>(chartData);
        }

        public async Task<ApiResult<ChartData>> GetRevenueInNMonthOfYear(int year)
        {
            var query = from s in _context.Screenings
                        join r in _context.Reservations on s.Id equals r.ScreeningId
                        join t in _context.Tickets on r.Id equals t.ReservationId
                        join rt in _context.ReservationTypes on r.TypeId equals rt.Id
                        where r.Time.Year == year
                        select new { rt, t, r };

            var revenue = await query.GroupBy(x => new { x.r.Time.Month })
                .Select(x => new
                {
                    Month = x.Key.Month,
                    Revenue = x.Sum(cb => cb.t.Price)
                }).ToListAsync();

            for (int i = 1; i <= 12; i++)
            {
                if (!revenue.Exists(x => x.Month == i))
                    revenue.Add(new
                    {
                        Month = i,
                        Revenue = (decimal)0
                    });
            }

            revenue.OrderBy(x => x.Month);
            ChartData chartData = new ChartData();
            chartData.Lables = revenue.Select(x => x.Month.ToString()).ToList();
            chartData.DataRows[0] = revenue.Select(x => x.Revenue).ToList();
            return new ApiSuccessResult<ChartData>(chartData);
        }

        private DateTime GetDateEndMonth(DateTime date)
        {
            var temp = date.AddMonths(1);
            var startNextMounth = new DateTime(temp.Year, temp.Month, 1);
            DateTime endDate = temp.AddDays(-1);
            return endDate;
        }

        private DateTime GetDateStartMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }
    }
}