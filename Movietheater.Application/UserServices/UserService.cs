using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
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

namespace Movietheater.Application.UserServices
{
    public class UserService : IUserService
    {
        private readonly MovieTheaterDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<AppRole> _roleManager;

        public UserService(MovieTheaterDBContext context, UserManager<AppUser> userManager
            , SignInManager<AppUser> signInManager, IConfiguration configuration, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }
        public async Task<ApiResult<string>> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return new ApiErrorResult<string>("Tên đăng nhập không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, request.RememberMe, false);
            if (result.Succeeded == false)
                return new ApiErrorResult<string>("Mật khẩu không chính xác");

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),

            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
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

        public async Task<ApiResultLite> CreateAsync(UserCreateRequest model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return new ApiErrorResultLite("Email đã tồn tại");
            if (await _userManager.FindByNameAsync(model.UserName) != null)
                return new ApiErrorResultLite("UserName đã tồn tại");

            //var uid = Guid.NewGuid();
            //while (await _userManager.FindByIdAsync(uid.ToString()) != null)
            //{
            //    uid = Guid.NewGuid();
            //}

            AppUser user = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName,
                Email = model.Email,

            };
            if ((await _userManager.CreateAsync(user, model.Password)).Succeeded)
            {
                return new ApiSuccessResultLite();
            }
            return new ApiErrorResultLite("Tạo mới thất bại");

        }
        public async Task<ApiResultLite> UpdateAsync(UserUpdateRequest model)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == model.Email && x.Id != model.Id))
            {
                return new ApiErrorResultLite("Emai đã tồn tại");
            }

            var user = await _userManager.FindByIdAsync(model.Id.ToString());

            user.Dob = model.Dob;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Status = model.Status;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApiSuccessResultLite();
            }
            else
                return new ApiErrorResultLite("Cập nhật không thành công");
        }
        public async Task<ApiResultLite> DeleteAsync(Guid Id)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            if (user == null)
                return new ApiErrorResultLite("Không tìm thấy người dùng");
            else
            {
                if (_userManager.GetRolesAsync(user) == null &&
                    _context.Reservations.Where(x => x.EmployeeId == Id) == null)
                {
                    var reservations = _context.Reservations.Where(x => x.UserId == Id);
                    foreach (var reservation in reservations)
                    {
                        reservation.UserId = null;
                    }
                    _context.SaveChanges();
                    await _userManager.DeleteAsync(user);

                }
                else
                {
                    user.Status = Status.InActive;
                }
                return new ApiSuccessResultLite();
            }
        }
        public async Task<ApiResultLite> ChangePasswordAsync(ChangePWRequest request)
        {

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                //Check confirm password
                return new ApiErrorResultLite("Tên đăng nhập không tồn tại");
            }
            else
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, request.OldPassword, false);
                if (result.Succeeded == false)
                    return new ApiErrorResultLite("Mật khẩu không chính xác");
                else
                {
                    await _userManager.RemovePasswordAsync(user);
                    await _userManager.AddPasswordAsync(user, request.NewPassword);
                    return new ApiSuccessResultLite("Đổi password thành công");
                }

            }

        }
        public async Task<ApiResultLite> RoleAssignAsync(RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                return new ApiErrorResultLite("Tài khoản không tồn tại");
            }

            // check role available 
            List<string> roles = request.Roles.Select(x => x.Id).ToList();
            if (!CheckRoles(roles))
            {
                return new ApiErrorResultLite("Yêu cầu không hợp lệ");
            }

            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }

            var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    if (!(await _userManager.AddToRoleAsync(user, roleName)).Succeeded)
                    {
                        return new ApiErrorResultLite("Xảy ra lỗi");
                    }
                }
            }

            return new ApiSuccessResultLite();
        }
        public async Task<ApiResult<UserVMD>> GetUserByIdAsync (string id)
        {
            var user = await  _userManager.FindByIdAsync(id);
            if(user == null)
            {
                return new ApiErrorResult<UserVMD>("Người dùng không tồn tại");
            }

            var userVMD = new UserVMD()
            {
                Dob = user.Dob,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                Status = user.Status,
                UserName = user.UserName,
                Roles = (List<string>)await _userManager.GetRolesAsync(user)
            };

            return new ApiSuccessResult<UserVMD>(userVMD);
        }
        public async Task<ApiResult<PageResult<UserVMD>>> GetUserPagingAsync(UserPagingRequest request)
        {
            var users = _context.Users.Select(x => x);
            if(!string.IsNullOrWhiteSpace(request.RoleId))
            {
                users = users.Join(_context.AppUserRoles,
                               u => u.Id,
                               ur => ur.UserId,
                               (u, ur) => new { u, ur }).Where(x => request.RoleId == x.ur.RoleId.ToString()).Select(x => x.u);
            }
           

            if (request.Status != null)
            {
                users = users.Where(x => x.Status == request.Status);
            }

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                users = users.Where(x => x.UserName.Contains(request.Keyword)
                                      || x.PhoneNumber.Contains(request.Keyword)
                                      || x.FirstName.Contains(request.Keyword)
                                      || x.LastName.Contains(request.Keyword)
                                      || x.Email.Contains(request.Keyword)
                                      || x.Dob.ToString().Contains(request.Keyword));
            }

            int totalRow = await users.CountAsync();
            var item = users.OrderBy(x => x.UserName).Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageIndex).Select(x => new UserVMD()
                {
                    Id = x.Id,
                    Dob = x.Dob,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.UserName,
                    Status = x.Status
                }).ToList();

            var pageResult = new PageResult<UserVMD>()
            {

                TotalRecord = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Item = item,
            };

            return new ApiSuccessResult<PageResult<UserVMD>>(pageResult);

        }
        private bool CheckRoles(List<string> roles)
        {
            var listRole = _roleManager.Roles.Select(x => x.Id.ToString()).ToList();

            if (roles != null)
            {
                if (listRole == null)
                    return false;
                foreach (var role in roles)
                {
                    if (!CheckRole(role, listRole))
                    {
                        return false;
                    }
                }

            }
            return true;
        }
        private bool CheckRole(string role, List<string> roles)
        {
            for (int i = 0; i <= 0; i++)
            {
                if (role == roles[i])
                    return true;
            }
            return false;
        }

      
    }
}
