using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieTheater.Application.MailServices;
using MovieTheater.Data.Enums;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Identity.Role;
using MovieTheater.Models.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MovieTheater.Common.Constants;
using MovieTheater.Common.Helper;
using MovieTheater.Data.Models;

namespace MovieTheater.Application.UserServices
{
    public class UserService : IUserService
    {
        private readonly MoviesContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;

        public UserService(MoviesContext context, IHttpContextAccessor accessor, IMailService mailService, IConfiguration configuration)
        {
            _context = context;
            _accessor = accessor;
            _mailService = mailService;
            _configuration = configuration;
        }

        public async Task<ApiResult<string>> LoginAsync(LoginRequest request)
        {
            List<Claim> claims;

            var staff = await _context.Staffs.Where(x => x.UserName == request.Email).FirstOrDefaultAsync() ?? await _context.Staffs.Where(x => x.Mail == request.Email).FirstOrDefaultAsync();

            if (staff == null)
                return new ApiErrorResult<string>("Email không tồn tại trên hệ thống");

            if (staff.Password != request.Password.Encrypt())
                return new ApiErrorResult<string>("Mật khẩu không chính xác");



            claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, staff.UserName),
                new Claim(ClaimTypes.Name, $"{staff.LastName} {staff.FirstName}"),
                new Claim(ClaimTypes.Email, staff.Mail),
                new Claim(ClaimTypes.Role, staff.Role)
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

            return new ApiSuccessResult<string>(token);
        }

        public async Task<ApiResult<bool>> CreateAsync(UserRegisterRequest request)
        {
            var user = await _context.Staffs.Where(x => x.Mail == request.Email).FirstOrDefaultAsync();
            if (user != null)
                return new ApiErrorResult<bool>("Email đã tồn tại");

            var resGetByUserName = await _context.Staffs.Where(x => x.UserName == request.UserName).FirstOrDefaultAsync();
            if (resGetByUserName != null)
                return new ApiErrorResult<bool>("Username đã tồn tại");

            var maxIndex = await _context.Customers.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefaultAsync();
            var staff = new staff()
            {
                Dob = request.Dob,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Mail = request.Email,
                Password = request.Password.Encrypt(),
                Phone = request.PhoneNumber,
                UserName = request.UserName,
                Role = request.Role
            };

            _context.Staffs.Add(staff);
            int res = await _context.SaveChangesAsync();
            if (res == 0)
                return new ApiErrorResult<bool>("Thất bại");
            else
            {
                return new ApiSuccessResult<bool>(true);
            }
        }

        public Task<ApiResult<bool>> UpdateAsync(UserUpdateRequest model)
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

        public Task<ApiResult<bool>> RoleAssignAsync(RoleAssignRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<UserVMD>> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<PageResult<UserVMD>>> GetUserPagingAsync(UserPagingRequest request)
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