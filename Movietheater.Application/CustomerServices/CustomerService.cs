using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieTheater.Application.MailServices;
using MovieTheater.Common.Constants;
using MovieTheater.Common.Helper;
using MovieTheater.Data.Models;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.User;

namespace MovieTheater.Application.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;

        public CustomerService(MoviesContext context, IHttpContextAccessor accessor, IMailService mailService,
            IConfiguration configuration)
        {
            _accessor = accessor;
            _mailService = mailService;
            _configuration = configuration;
        }

        public async Task<ApiResult<string>> LoginAsync(LoginRequest request)
        {
            List<Claim> claims;

            await using (var context = new MoviesContext())
            {
                var user = await context.Customers.Where(x => x.Mail == request.Email).FirstOrDefaultAsync();

                if (user == null)
                    return new ApiErrorResult<string>("Email không tồn tại trên hệ thống");

                if (user.Password != request.Password.Encrypt())
                    return new ApiErrorResult<string>("Mật khẩu không chính xác");


                claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, $"{user.LastName} {user.FirstName}"),
                    new Claim(ClaimTypes.Email, user.Mail),
                    new Claim(ClaimTypes.Role, RoleConstant.Customer)
                };
            }


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(_configuration["JWT:ValidIssuer"],
                _configuration["JWT:validAudience"],
                claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(jwtSecurityToken);

            return new ApiSuccessResult<string>(token);
        }

        public async Task<ApiResult<bool>> RegisterAsync(UserRegisterRequest request)
        {
            await using (var context = new MoviesContext())
            {
                var user = await context.Customers.Where(x => x.Mail == request.Email).FirstOrDefaultAsync();
                if (user != null)
                    return new ApiErrorResult<bool>("Email đã tồn tại");

                var maxIndex = await context.Customers.OrderByDescending(x => x.Id).Select(x => x.Id)
                    .FirstOrDefaultAsync();
                var customer = new Customer()
                {
                    Dob = request.Dob,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Id = CreateCustomerUser(maxIndex),
                    Mail = request.Email,
                    Password = request.Password.Encrypt(),
                    Phone = request.PhoneNumber
                };

                context.Customers.Add(customer);
                int res = await context.SaveChangesAsync();
                if (res == 0)
                    return new ApiErrorResult<bool>("Thất bại");
                else
                {
                    return new ApiSuccessResult<bool>(true);
                }
            }
        }

        public Task<ApiResult<bool>> UpdateAsync(UserUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> ChangePasswordAsync(ChangePwRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<CustomerVMD>> GetByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<PageResult<CustomerVMD>>> GetUserPagingAsync(UserPagingRequest request)
        {
            throw new NotImplementedException();
        }

        public ApiResult<bool> CheckToken(Guid userId, string token)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> ForgotPasswordAsync(string mail)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            throw new NotImplementedException();
        }

        public string CreateCustomerUser(string currentIndex)
        {
            if (string.IsNullOrEmpty(currentIndex))
                return "KH000000001";

            int number = Int32.Parse(currentIndex.Substring(2));
            number += 1;
            return $"KH{number.ToString().PadRight(9, '0')}";
        }
    }
}