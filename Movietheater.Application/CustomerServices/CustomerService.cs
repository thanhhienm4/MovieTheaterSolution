using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieTheater.Application.MailServices;
using MovieTheater.Data.Models;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.User;

namespace MovieTheater.Application.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly MoviesContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;
        public CustomerService(MoviesContext context, IHttpContextAccessor accessor, IMailService mailService, IConfiguration configuration)
        {
            _context = context;
            _accessor = accessor;
            _mailService = mailService;
            _configuration = configuration;
        }

        public async Task<ApiResult<string>> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return new ApiErrorResult<string>("Tên đăng nhập không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, request.RememberMe, false);

            if (result.IsLockedOut == true)
                return new ApiErrorResult<string>("Tài khoản của bạn đã bị vô hiệu hóa");

            if (result.Succeeded == false)
                return new ApiErrorResult<string>("Mật khẩu không chính xác");


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };
            


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(_configuration["JWT:ValidIssuer"],
                _configuration["JWT:validAudience"],
              claims,
              expires: DateTime.Now.AddMonths(1),
              signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(jwtSecurityToken);

            // Save Token
            var userToken = await _context.UserTokens.FindAsync(user.Id);
            if (userToken == null)
            {
                await _context.UserTokens.AddAsync(new IdentityUserToken<Guid>()
                {
                    UserId = user.Id,
                    Value = token
                });
            }
            else
            {
                userToken.Value = token;
                _context.UserTokens.Update(userToken);
            }
            _context.SaveChanges();

            return new ApiSuccessResult<string>(token);
        }

        public Task<ApiResult<bool>> CreateAsync(UserCreateRequest request)
        {
            throw new NotImplementedException();
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
    }
}
