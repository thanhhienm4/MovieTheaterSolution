using System;
using System.Collections.Generic;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.ChartTable;
using System.Threading.Tasks;
using MovieTheater.Models.Catalog.Invoice;

namespace MovieTheater.Application.Statistic
{
    public interface IStatisticService
    {
        Task<ApiResult<ChartData>> GetTopRevenueFilmAsync(CalRevenueRequest request);

        Task<ApiResult<long>> GetRevenueAsync(CalRevenueRequest request);

        Task<ApiResult<ChartData>> GetRevenueTypeAsync(CalRevenueRequest request);

        Task<ApiResult<ChartData>> GetRevenueInNMonthAsync(int n);
        Task<ApiResult<IList<InvoiceRawData>>> GetRawData(DateTime fromDate, DateTime toDate);
        Task<ApiResult<ChartData>> GetRevenueDayInWeek(DateTime fromDate, DateTime toDate);
    }
}