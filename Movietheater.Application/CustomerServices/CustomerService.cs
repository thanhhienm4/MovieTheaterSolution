using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieTheater.Application.MailServices;
using MovieTheater.Common.Constants;
using MovieTheater.Common.Helper;
using MovieTheater.Data.Models;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utils = MovieTheater.Common.Helper.Utils;

namespace MovieTheater.Application.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;
        private readonly MoviesContext _context;

        public CustomerService(MoviesContext context, IHttpContextAccessor accessor, IMailService mailService,
            IConfiguration configuration)
        {
            _accessor = accessor;
            _mailService = mailService;
            _configuration = configuration;
            _context = context;
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

        public async Task<ApiResult<bool>> UpdateAsync(UserUpdateRequest request)
        {
            var customer = await _context.Customers.Where(x => x.Mail == request.Email).FirstOrDefaultAsync();
            if (customer == null)
                return new ApiErrorResult<bool>("Không tồn tại user");
            else
            {
                customer.Dob = request.Dob;
                customer.FirstName = request.FirstName;
                customer.LastName = request.LastName;
                customer.Mail = request.Email;
                customer.Phone = request.PhoneNumber;

                _context.Update(customer);
                if (await _context.SaveChangesAsync() > 0)
                    return new ApiSuccessResult<bool>(true);
                return new ApiErrorResult<bool>("Cập nhật thất bại");
            }
        }

        public Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<bool>> ChangePasswordAsync(ChangePwRequest request)
        {
            var customer = await _context.Customers.Where(x => x.Mail == request.UserName).FirstOrDefaultAsync();
            if (customer == null)
                return new ApiErrorResult<bool>("Không tồn tại user");
            else
            {
                if (request.OldPassword.Encrypt() != customer.Password)
                    return new ApiErrorResult<bool>("Sai mật khẩu");
                customer.Password = request.NewPassword.Encrypt();

                if (await _context.SaveChangesAsync() > 0)
                    return new ApiSuccessResult<bool>(true);
                return new ApiErrorResult<bool>("Cập nhật thất bại");
            }
        }

        public async Task<ApiResult<CustomerVMD>> GetById(string id)
        {
            var user = await _context.Customers.Where(x => x.Id == id).Select(x => new CustomerVMD()
            {
                Dob = x.Dob,
                Email = x.Mail,
                FirstName = x.FirstName,
                Id = x.Id,
                PhoneNumber = x.Phone,
                LastName = x.LastName,
            }).FirstOrDefaultAsync();
            if (user == null)
                return new ApiErrorResult<CustomerVMD>("Không tìm thấy người dùng");
            else
            {
                return new ApiSuccessResult<CustomerVMD>(user);
            }
        }

        public Task<ApiResult<PageResult<CustomerVMD>>> GetUserPagingAsync(UserPagingRequest request)
        {
            throw new NotImplementedException();
        }

        public ApiResult<bool> CheckToken(Guid userId, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<bool>> ForgotPasswordAsync(string mail)
        {
            var customer = await _context.Customers.Where(x => x.Mail == mail).FirstOrDefaultAsync();
            if (customer == null)
                return new ApiErrorResult<bool>("Không tồn tại user");
            else
            {
                var password = Utils.RandPassword();
                customer.Password = password.Encrypt();

                await _mailService.SendEmailAsync(customer.Mail, "Khôi phục mật khẩu",
                    "Mật khẩu mới tại GG Theater của bạn là: " + password);
                

                if (await _context.SaveChangesAsync() > 0)
                    return new ApiSuccessResult<bool>(true);
                return new ApiErrorResult<bool>("Thất bại");
            }
        }

        public Task<ApiResult<bool>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<IList<CustomerTypeVmd>>> GetAllCustomerType()
        {
            var res = await _context.CustomerTypes.Select(x => new CustomerTypeVmd(x)).ToListAsync();
            return new ApiSuccessResult<IList<CustomerTypeVmd>>(res);
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