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

        public async Task<ApiResult<ChartData>> GetTopGrossingFilmAsync(TopGrossingFilmRequest request)
        {
            var query = from s in _context.Screenings
                        join f in _context.Films on s.FilmId equals f.Id
                        join t in _context.Tickets on s.Id equals t.ScreeningId
                        where s.TimeStart.Date > request.StartDate && s.TimeStart.Date < request.EndDate
                        select new { s,f,t};

            var grossing = await query.GroupBy(x => new { x.f.Name,x.f.Id}).Select(x => new 
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
