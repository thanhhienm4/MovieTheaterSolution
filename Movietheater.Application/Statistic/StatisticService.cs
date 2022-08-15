using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.Models;
using MovieTheater.Models.Catalog.Invoice;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.ChartTable;

namespace MovieTheater.Application.Statistic
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
                where r.Time.Date >= request.StartDate.Date && r.Time.Date <= request.EndDate.Date &&
                      s.Active == true && r.Active == true
                select new { s, m, t };

            var Revenue = await query.GroupBy(x => new { x.m.Name, x.m.Id }).Select(x => new
            {
                Name = x.Key.Name,
                Revenue = (decimal)x.Sum(sft => sft.t.Price)
            }).OrderByDescending(x => x.Revenue).ToListAsync();

            ChartData 
                chartData = new ChartData();
            chartData.Lables = Revenue.Select(x => x.Name).ToList();
            chartData.DataRows[0] = Revenue.Select(x => x.Revenue).ToList();
            return new ApiSuccessResult<ChartData>(chartData);
        }

        public async Task<ApiResult<long>> GetRevenueAsync(CalRevenueRequest request)
        {
            var query = _context.Invoices.Where(i => i.Date.Date > request.StartDate && i.Date.Date < request.EndDate);

            long revenue = (long)await query.SumAsync(x => x.Price);
            return new ApiSuccessResult<long>(revenue);
        }

        public async Task<ApiResult<ChartData>> GetRevenueTypeAsync(CalRevenueRequest request)
        {
            var query = from i in _context.Invoices
                join r in _context.Reservations on i.ReservationId equals r.Id
                join rt in _context.ReservationTypes on r.TypeId equals rt.Id
                where i.Date.Date > request.StartDate && i.Date.Date < request.EndDate
                select new { rt, i};

            var revenue = await query.GroupBy(x => new { x.rt.Name }).Select(x => new
            {
                RowName = x.Key.Name,
                Revenue = (decimal)x.Sum(sft => sft.i.Price)
            }).ToListAsync();

            ChartData chartData = new ChartData();
            chartData.Lables = revenue.Select(x => x.RowName).ToList();
            chartData.DataRows[0] = revenue.Select(x => x.Revenue).ToList();

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
            var query =  _context.Invoices.Where(x => x.Date.Year == year);

            var revenue = await query.GroupBy(x => new { x.Date.Month })
                .Select(x => new
                {
                    Month = x.Key.Month,
                    Revenue = x.Sum(cb => cb.Price)
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
            ChartData chartData = new ChartData
            {
                Lables = revenue.Select(x => x.Month.ToString()).ToList(),
                DataRows =
                {
                    [0] = revenue.Select(x => x.Revenue).ToList()
                }
            };
            return new ApiSuccessResult<ChartData>(chartData);
        }

        private DateTime GetDateEndMonth(DateTime date)
        {
            var temp = date.AddMonths(1);
            var startNextMonth = new DateTime(temp.Year, temp.Month, 1);
            DateTime endDate = temp.AddDays(-1);
            return endDate;
        }

        private DateTime GetDateStartMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public async Task<ApiResult<IList<InvoiceRawData>>> GetRawData(DateTime fromDate, DateTime toDate)
        {
            var res = await _context.Invoices
                .Where(x => x.Date.Date  >= fromDate.Date && x.Date.Date <= toDate.Date)
                .Include(x => x.Payment)
                .Include(x => x.Reservation)
                .Include(x => x.Reservation.Screening)
                .Include(x => x.Reservation.Screening.Movie)
                .Include(x => x.Reservation.Screening.Auditorium.Format)
                .Include(x => x.Reservation.Tickets)
                .Select(x => new InvoiceRawData()
                {
                    MovieId = x.Reservation.Screening.MovieId,
                    MovieName = x.Reservation.Screening.Movie.Name,
                    Date = x.Date,
                    InvoiceId = x.Id,
                    Payment = x.Payment.Name,
                    TotalPrice = x.Price,
                    ReservationId = x.ReservationId,
                    ScreeningTime = x.Reservation.Screening.StartTime,
                    Tickets = x.Reservation.Tickets.Count
                }).ToListAsync();
            return new ApiSuccessResult<IList<InvoiceRawData>> (res);
        }

        public async Task<ApiResult<ChartData>> GetRevenueDayInWeek(DateTime fromDate, DateTime toDate)
        {
            var firstSunday = new DateTime();
            while (firstSunday.DayOfWeek != DayOfWeek.Sunday)
            {
                firstSunday = firstSunday.AddDays(1);
            }

            var revenue  = await _context.Invoices
                .Include(x => x.Reservation)
                .ThenInclude(x => x.Screening)
                .Where(x => x.Reservation.Screening.StartTime.Date >= fromDate.Date && x.Reservation.Screening.StartTime.Date <= toDate.Date)
                .GroupBy(x => EF.Functions.DateDiffDay(firstSunday, x.Reservation.Screening.StartTime.Date)%7)
                .Select(x => new { Day = x.Key, Revenue = x.Sum(invoice => invoice.Price) }).ToListAsync();

            var charData = new ChartData
            {
                Lables = revenue.Select(x => firstSunday.AddDays(x.Day).ToString("ddd", new CultureInfo("vi-VN"))).ToList(),
                DataRows =
                {
                    [0] = revenue.Select(x => x.Revenue).ToList()
                }
            };
            return new ApiSuccessResult<ChartData> (charData);
        }

        



    }
}