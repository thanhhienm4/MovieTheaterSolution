using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.ChartTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.Statitic
{
    public interface IStatiticService
    {
        Task<ApiResult<ChartData>> GetTopGrossingFilmAsync(TopGrossingFilmRequest request);
    }
}
