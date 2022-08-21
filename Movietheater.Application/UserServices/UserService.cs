using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieTheater.Application.MailServices;
using MovieTheater.Common.Helper;
using MovieTheater.Data.Enums;
using MovieTheater.Data.Models;
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

namespace MovieTheater.Application.UserServices
{
    public class UserService : IUserService
    {
        private readonly MoviesContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;

        public UserService(MoviesContext context, IHttpContextAccessor accessor, IMailService mailService,
            IConfiguration configuration)
        {
            _context = context;
            _accessor = accessor;
            _mailService = mailService;
            _configuration = configuration;
        }

        public async Task<ApiResult<string>> LoginAsync(LoginRequest request)
        {
            List<Claim> claims;

            var staff = await _context.Staffs.Where(x => x.UserName == request.Email).FirstOrDefaultAsync() ??
                        await _context.Staffs.Where(x => x.Mail == request.Email).FirstOrDefaultAsync();

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

            var resGetByUserName =
                await _context.Staffs.Where(x => x.UserName == request.UserName).FirstOrDefaultAsync();
            if (resGetByUserName != null)
                return new ApiErrorResult<bool>("Username đã tồn tại");

            var maxIndex = await _context.Customers.OrderByDescending(x => x.Id).Select(x => x.Id)
                .FirstOrDefaultAsync();
            var staff = new staff()
            {
                Dob = request.Dob,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Mail = request.Email,
                Password = request.Password.Encrypt(),
                Phone = request.PhoneNumber,
                UserName = request.UserName,
                Role = request.Role,
                Active = true
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

        public async Task<ApiResult<bool>> UpdateAsync(UserUpdateRequest model)
        {
            var staff = await _context.Staffs.FindAsync(model.UserName);
            if (staff == null)
                return new ApiErrorResult<bool>("Không tồn tại user");
            else
            {
                staff.Active = model.Status == Status.Active ? true : false;
                staff.Dob = model.Dob;
                staff.FirstName = model.FirstName;
                staff.LastName = model.LastName;
                staff.Mail = model.Email;
                staff.Role = model.Role;
                staff.Phone = model.PhoneNumber;

                _context.Update(staff);
                if (await _context.SaveChangesAsync() > 0)
                    return new ApiSuccessResult<bool>(true);
                return new ApiErrorResult<bool>("Cập nhật thất bại");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(string username)
        {
            var staff = await _context.Staffs.FindAsync(username);
            if (staff == null)
                return new ApiErrorResult<bool>("Không tồn tại user");
            else
            {
                staff.Active = false;
                _context.Staffs.Update(staff);
                if (await _context.SaveChangesAsync() > 0)
                    return new ApiSuccessResult<bool>(true);
                return new ApiErrorResult<bool>("Xóa thất bại thất bại");
            }
        }

        public async Task<ApiResult<bool>> ChangePasswordAsync(ChangePwRequest request)
        {
            var staff = await _context.Staffs.FindAsync(request.UserName);
            if (staff == null)
                return new ApiErrorResult<bool>("Không tồn tại user");
            else
            {
                if (request.OldPassword.Encrypt() != staff.Password)
                    return new ApiErrorResult<bool>("Sai mật khẩu");

                staff.Password = request.NewPassword.Encrypt();
                
                if (await _context.SaveChangesAsync() > 0)
                    return new ApiSuccessResult<bool>(true);
                return new ApiErrorResult<bool>("Cập nhật thất bại");
            }
        }

        public async Task<ApiResult<UserVMD>> GetUserByIdAsync(string id)
        {
            var user = await _context.Staffs.Where(x => x.UserName == id).Select(x => new UserVMD()
            {
                Dob = x.Dob,
                Email = x.Mail,
                FirstName = x.FirstName,
                UserName = x.UserName,
                PhoneNumber = x.Phone,
                LastName = x.LastName,
                Role = x.Role,
                Status = x.Active ? Status.Active : Status.InActive
            }).FirstOrDefaultAsync();
            if (user == null)
                return new ApiErrorResult<UserVMD>("Không tìm thấy người dùng");
            else
            {
                return new ApiSuccessResult<UserVMD>(user);
            }
        }

        public async Task<ApiResult<PageResult<UserVMD>>> GetUserPagingAsync(UserPagingRequest request)
        {
            var query = _context.Staffs.Select(x => new UserVMD()
            {
                Dob = x.Dob,
                Email = x.Mail,
                FirstName = x.FirstName,
                UserName = x.UserName,
                PhoneNumber = x.Phone,
                LastName = x.LastName,
                Role = x.Role,
                Status = x.Active ? Status.Active : Status.InActive
            });

            if (!string.IsNullOrEmpty(request.RoleId))
                query = query.Where(x => x.Role == request.RoleId);

            if (!string.IsNullOrWhiteSpace(request.Keyword))
                query = query.Where(x => x.Email.Contains(request.Keyword)
                                         || x.PhoneNumber.Contains(request.Keyword)
                                         || x.UserName.Contains(request.Keyword)
                                         || x.FirstName.Contains(request.Keyword));

            var total = query.Count();
            var res = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList();
            var pageResult = new PageResult<UserVMD>()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Item = res,
                TotalRecord = total
            };
            return new ApiSuccessResult<PageResult<UserVMD>>(pageResult);
        }

        public async Task<ApiResult<bool>> ForgotPasswordAsync(string mail)
        {
            var staff = await _context.Staffs.Where(x => x.Mail == mail).FirstOrDefaultAsync();
            if (staff == null)
                return new ApiErrorResult<bool>("Không tồn tại user");
            else
            {
                var password = Utils.RandPassword();
                staff.Password = password.Encrypt();

                await _mailService.SendEmailAsync(staff.Mail, "Khôi phục mật khẩu",
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
    }
}