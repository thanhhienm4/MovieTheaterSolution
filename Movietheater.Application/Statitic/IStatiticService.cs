using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.ChartTable;
using System.Threading.Tasks;

namespace Movietheater.Application.Statitic
{
    public interface IStatiticService
    {
        Task<ApiResult<ChartData>> GetTopRevenueFilmAsync(CalRevenueRequest request);

        Task<ApiResult<long>> GetRevenueAsync(CalRevenueRequest request);

        Task<ApiResult<ChartData>> GetRevenueTypeAsync(CalRevenueRequest request);

        Task<ApiResult<ChartData>> GetRevenueInNMonthAsync(int n);
    }
}