using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.ChartTable;
using System.Net.Http;
using System.Threading.Tasks;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Catalog.Invoice;

namespace MovieTheater.Api
{
    public class StatisticApiClient : BaseApiClient
    {
        public StatisticApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        {
        }

        public async Task<ApiResult<ChartData>> GetTopRevenueFilmAsync(CalRevenueRequest request)
        {
            return await PostAsync<ChartData>("Api/Statistic/GetTopRevenueFilm", request);
        }

        public async Task<ApiResult<long>> GetRevenueAsync(CalRevenueRequest request)
        {
            return await PostAsync<long>("Api/Statistic/GetRevenueAsync", request);
        }

        public async Task<ApiResult<ChartData>> GetRevenueTypeAsync(CalRevenueRequest request)
        {
            return await PostAsync<ChartData>("Api/Statistic/GetRevenueTypeAsync", request);
        }

        public async Task<ApiResult<IList<InvoiceRawData>>> GetRawData(DateTime fromDate, DateTime toDate)
        {
            var param = new NameValueCollection()
            {
                { "fromDate", fromDate.ToString(CultureInfo.InvariantCulture) },
                { "toDate", toDate.ToString(CultureInfo.InvariantCulture) },
            };
            return await GetAsync<IList<InvoiceRawData >> ($"{ApiConstant.ApiStatistic}/{ApiConstant.StatisticGetRawData}",param);
        }
        public async Task<ApiResult<ChartData>> GetRevenueInWeek(DateTime fromDate, DateTime toDate)
        {
            var param = new NameValueCollection()
            {
                { "fromDate", fromDate.ToString(CultureInfo.InvariantCulture) },
                { "toDate", toDate.ToString(CultureInfo.InvariantCulture) },
            };
            return await GetAsync<ChartData>($"{ApiConstant.ApiStatistic}/{ApiConstant.StatisticGetRevenueDayInWeek}", param);
        }

    }
}