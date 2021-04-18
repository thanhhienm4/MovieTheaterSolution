using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    public class FilmApiClient : BaseApiClient
    {
        public FilmApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
           IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        { }
        public async Task<ApiResultLite> CreateAsync(FilmCreateRequest request)
        {

            var requestContent = new MultipartFormDataContent();

            if (request.Poster != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.Poster.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.Poster.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "Poster", request.Poster.FileName);
            }
            requestContent.Add(new StringContent(request.Description.ToString()), "Description");
            requestContent.Add(new StringContent(request.BanId.ToString()), "BanId");
            requestContent.Add(new StringContent(request.Length.ToString()), "Length");
            requestContent.Add(new StringContent(request.Name.ToString()), "Name");
            requestContent.Add(new StringContent(request.PublishDate.ToString()), "PublishDate");
            requestContent.Add(new StringContent(request.TrailerURL.ToString()), "TrailerURL");
            HttpClient client = GetHttpClient();

            var response = await client.PostAsync($"Api/Film/Create", requestContent);

            return await GetResultAsync<ApiResultLite>(response);
            

        }
        public async Task<ApiResultLite> UpdateAsync(FilmUpdateRequest request)
        {
            var requestContent = new MultipartFormDataContent();

            if (request.Poster != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.Poster.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.Poster.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "Poster", request.Poster.FileName);
            }
            requestContent.Add(new StringContent(request.Description.ToString()), "Description");
            requestContent.Add(new StringContent(request.Id.ToString()), "Id");
            requestContent.Add(new StringContent(request.BanId.ToString()), "BanId");
            requestContent.Add(new StringContent(request.Length.ToString()), "Length");
            requestContent.Add(new StringContent(request.Name.ToString()), "Name");
            requestContent.Add(new StringContent(request.PublishDate.ToString()), "PublishDate");
            requestContent.Add(new StringContent(request.TrailerURL.ToString()), "TrailerURL");
            //return await PutAsync<ApiResultLite>("Api/Film/Update", requestContent);

            HttpClient client = GetHttpClient();
            var response = await client.PutAsync($"Api/Film/Update", requestContent);

            return await GetResultAsync<ApiResultLite>(response);
        }
        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            return await DeleteAsync<ApiResultLite>($"Api/Film/Delete/{id}");
        }
        public async Task<ApiResult<PageResult<FilmVMD>>> GetFilmPagingAsync(FilmPagingRequest request)
        {
            return await PostAsync<ApiResult<PageResult<FilmVMD>>>($"Api/Film/GetFilmPaging", request);
        }

        public async Task<ApiResult<FilmMD>> GetFilmMDByIdAsync(int id)
        {
            return await GetAsync<ApiResult<FilmMD>>($"Api/Film/GetFilmMDById/{id}");
        }
        public async Task<ApiResult<List<FilmVMD>>> GetAllFilmAsync()
        {
            return await GetAsync<ApiResult<List<FilmVMD>>>($"Api/Film/GetAllFilm");
        }

    }
}
