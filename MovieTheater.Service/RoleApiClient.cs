using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Identity.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    public class RoleApiClient : BaseApiClient
    {
        public RoleApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
    IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
     httpContextAccessor)
        { }
       public async Task<List<RoleVMD>> GetRolesAsync()
       {
            return await GetAsync<List<RoleVMD>>("/api/Role/GetAll");
       }
    }
}
