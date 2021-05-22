using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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
        protected async Task<TResponse> GetAsync<TResponse>(string url)
        {
            HttpClient client = GetHttpClient();
            var response = await client.GetAsync(url);
            return await GetResultAsync<TResponse>(response);
        }

        protected async Task<List<List<TResponse>>> GetMatrixAsync<TResponse>(string url)
        {
            HttpClient client = GetHttpClient();
            var response = await client.GetAsync(url);
            return await GetResultAsync<List<List<TResponse>>>(response);
        }

        // post data to Api
        protected async Task<TResponse> PostAsync<TResponse>(string url, object obj)
        {
            HttpClient client = GetHttpClient();
            var json = JsonConvert.SerializeObject(obj);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, httpContent);
            return await GetResultAsync<TResponse>(response);
        }

        // send delete request to API
        protected async Task<TResponse> DeleteAsync<TResponse>(string url)
        {
            HttpClient client = GetHttpClient();
            var response = await client.DeleteAsync(url);
            return await GetResultAsync<TResponse>(response);
        }

        // send update request to Api
        protected async Task<TResponse> PutAsync<TResponse>(string url, object obj)
        {
            HttpClient client = GetHttpClient();
            var json = JsonConvert.SerializeObject(obj);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(url, httpContent);
            return await GetResultAsync<TResponse>(response);
        }

        public HttpClient GetHttpClient()
        {
            var BearerToken = _httpContextAccessor
               .HttpContext
               .Request.Cookies["Token"];

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ServerBackEnd"]);

            client.DefaultRequestHeaders.Authorization =
               new AuthenticationHeaderValue("Bearer", BearerToken);

            return client;
        }

        public static async Task<TResponse> GetResultAsync<TResponse>(HttpResponseMessage response)
        {
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                TResponse myDeserializedObjList = (TResponse)JsonConvert
                    .DeserializeObject(body, typeof(TResponse));
                return myDeserializedObjList;
            }
            return JsonConvert.DeserializeObject<TResponse>(body);
        }
    }
}