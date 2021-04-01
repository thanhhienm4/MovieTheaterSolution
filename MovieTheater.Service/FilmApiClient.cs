using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
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
            return await PostAsync<ApiResultLite>("Api/Film/Create", request);
        }
        public async Task<ApiResultLite> UpdateAsync(FilmUpdateRequest request)
        {
            return await PutAsync<ApiResultLite>("Api/Film/Update", request);
        }
        public async Task<ApiResultLite> DeleteAsync(Guid id)
        {
            return await DeleteAsync<ApiResultLite>($"Api/Film/Delete/{id}");
        }
        public async Task<ApiResult<PageResult<FilmVMD>>> GetFilmPagingAsync(FilmPagingRequest request)
        {
            return await PostAsync<ApiResult<PageResult<FilmVMD>>>($"Api/Film/GetFilmPaging", request);
        }

        public async Task<ApiResult<FilmVMD>> GetFilmByIdAsync(int id)
        {
            return await GetAsync<ApiResult<FilmVMD>>($"Api/Film/GetFilmById/{id}");
        }

    }
}
