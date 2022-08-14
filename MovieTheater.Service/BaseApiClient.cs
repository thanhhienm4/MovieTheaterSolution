using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Common.ApiResult;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MovieTheater.Api
{
    public class BaseApiClient
    {
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly IConfiguration _configuration;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public BaseApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        // http get data form Api
        protected async Task<ApiResult<TResponse>> GetAsync<TResponse>(string url,
            NameValueCollection queryParams = null)
        {
            HttpClient client = GetHttpClient();
            var response = await client.GetAsync(GetRoute(url, queryParams));
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return await GetReLoginResultAsync<TResponse>();
            }

            return await GetResultAsync<TResponse>(response);
        }

        // post data to Api
        protected async Task<ApiResult<TResponse>> PostAsync<TResponse>(string url, object obj)
        {
            HttpClient client = GetHttpClient();
            var json = JsonConvert.SerializeObject(obj);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, httpContent);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return await GetReLoginResultAsync<TResponse>();
            }

            return await GetResultAsync<TResponse>(response);
        }

        // send delete request to API
        protected async Task<ApiResult<TResponse>> DeleteAsync<TResponse>(string url,
            NameValueCollection queryParams = null)
        {
            HttpClient client = GetHttpClient();
            var response = await client.DeleteAsync(GetRoute(url, queryParams));
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return await GetReLoginResultAsync<TResponse>();
            }

            return await GetResultAsync<TResponse>(response);
        }

        // send update request to Api
        protected async Task<ApiResult<TResponse>> PutAsync<TResponse>(string url, object obj)
        {
            HttpClient client = GetHttpClient();
            var json = JsonConvert.SerializeObject(obj);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(url, httpContent);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return await GetReLoginResultAsync<TResponse>();
            }

            return await GetResultAsync<TResponse>(response);
        }

        public HttpClient GetHttpClient()
        {
            var bearerToken = _httpContextAccessor
                .HttpContext
                .Request.Cookies["Token"];

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ServerBackEnd"]);

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", bearerToken);

            return client;
        }

        public static Task<ApiResult<TResponse>> GetReLoginResultAsync<TResponse>()
        {
            var res = new ApiResult<TResponse>
            {
                IsReLogin = true
            };
            return Task.FromResult(res);
        }

        public static async Task<ApiResult<TResponse>> GetResultAsync<TResponse>(HttpResponseMessage response)
        {
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                ApiResult<TResponse> myDeserializedObjList = (ApiResult<TResponse>)JsonConvert
                    .DeserializeObject(body, typeof(ApiResult<TResponse>));
                return myDeserializedObjList;
            }

            var res = JsonConvert.DeserializeObject<ApiResult<TResponse>>(body);
            res.IsReLogin = false;
            return res;
        }

        private string GetRoute(string url, NameValueCollection queryParams = null)
        {
            string route = url;

            if (queryParams is { Count: > 0 })
            {
                NameValueCollection httpValueCollection = HttpUtility.ParseQueryString(String.Empty);
                httpValueCollection.Add(queryParams);
                route = $"{route}?{httpValueCollection.ToString()}";
            }

            return route;
        }
    }
}