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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<AppRole> _roleManager;

        public UserService(MovieTheaterDBContext context, UserManager<User> userManager
            , SignInManager<User> signInManager, IConfiguration configuration, RoleManager<AppRole> roleManager)
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



            User user = new User()
            {
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName,
                Email = model.Email,
                UserInfor = new UserInfor()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                }

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

           
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.UserInfor = new UserInfor()
            {
                Id = user.Id,
                Dob = model.Dob,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Status = model.Status,
            };
           

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApiSuccessResultLite("Cập nhật thành công");
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
                var reservations =await _context.Reservations.Where(x => x.UserId == Id).ToListAsync();
                if ((await _userManager.GetRolesAsync(user)).Count() == 0 &&
                    _context.Reservations.Where(x => x.EmployeeId == Id).Count() == 0 )
                {
                    
                    foreach (var reservation in reservations)
                    {
                        reservation.UserId = null;
                    }
                    _context.SaveChanges();
                    await _userManager.DeleteAsync(user);

                }
                else
                {
                    await _userManager.SetLockoutEnabledAsync(user, true);
                }
                return new ApiSuccessResultLite("Xóa thành công");
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
            List<Guid> roles = request.Roles.Select(x => Guid.Parse(x.Id)).ToList();
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
            var userInfor = _context.UserInfors.Where(x => x.Id == user.Id).FirstOrDefault();

            var userVMD = new UserVMD()
            {
                Dob = userInfor.Dob,
                FirstName = userInfor.FirstName,
                LastName = userInfor.LastName,
                Email = user.Email,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,

                UserName = user.UserName,
                Roles = (List<string>)await _userManager.GetRolesAsync(user)
            };

            return new ApiSuccessResult<UserVMD>(userVMD);
        }
        public async Task<ApiResult<PageResult<UserVMD>>> GetUserPagingAsync(UserPagingRequest request)
        {
            var users = from u in _context.Users
                        join ui in _context.UserInfors on u.Id equals ui.Id
                        select new { u, ui };

            if(!string.IsNullOrWhiteSpace(request.RoleId))
            {
                users = users.Join(_context.UserRoles,
                               u => u.u.Id,
                               ur => ur.UserId,
                               (u, ur) => new { u, ur }).Where(x => request.RoleId == x.ur.RoleId.ToString()).Select(x => x.u);
            }
           


            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                users = users.Where(x => x.u.UserName.Contains(request.Keyword)
                                      || x.u.PhoneNumber.Contains(request.Keyword)
                                      || x.u.Email.Contains(request.Keyword)
                                      || x.ui.FirstName.Contains(request.Keyword)
                                      || x.ui.LastName.Contains(request.Keyword)                                   
                                      || x.ui.Dob.ToString().Contains(request.Keyword));
            }

            int totalRow = await users.CountAsync();
            var item = users.OrderBy(x => x.u.UserName).Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new UserVMD()
                {
                    Id = x.u.Id,
                    PhoneNumber = x.u.PhoneNumber,
                    UserName = x.u.UserName,
                    Email = x.u.Email,
                    Dob = x.ui.Dob,
                    FirstName = x.ui.FirstName,
                    LastName = x.ui.LastName,

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
        private bool CheckRoles(List<Guid> roles)
        {
            var listRole = _roleManager.Roles.Select(x => x.Id).ToList();

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
        private bool CheckRole(Guid role, List<Guid> roles)
        {
            for (int i = 0; i < roles.Count ; i++)
            {
                if (role == roles[i])
                    return true;
            }
            return false;
        }

      
    }
}
