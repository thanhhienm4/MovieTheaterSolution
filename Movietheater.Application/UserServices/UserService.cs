
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Movietheater.Application.MailServices;
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
using System.Security.Policy;
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
        private readonly IHttpContextAccessor _accessor;
        private readonly IMailService _mailServive;

        public UserService(MovieTheaterDBContext context, UserManager<User> userManager
            , SignInManager<User> signInManager, IConfiguration configuration, RoleManager<AppRole> roleManager,
            IHttpContextAccessor accessor, IMailService mailService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _accessor = accessor;
            _mailServive = mailService;
        }

        public async Task<ApiResult<string>> LoginStaffAsync(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return new ApiErrorResult<string>("Tên đăng nhập không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, request.RememberMe, false);

            if (result.IsLockedOut == true)
                return new ApiErrorResult<string>("Tài khoản của bạn đã bị vô hiệu hóa");

            if (result.Succeeded == false)
                return new ApiErrorResult<string>("Mật khẩu không chính xác");

            if (await _context.UserInfors.FindAsync(user.Id) == null)
                return new ApiErrorResult<string>("Tài khoản không tồn tại");

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

        public async Task<ApiResult<Guid>> CreateStaffAsync(UserCreateRequest model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return new ApiErrorResult<Guid>("Email đã tồn tại");
            if (await _userManager.FindByNameAsync(model.UserName) != null)
                return new ApiErrorResult<Guid>("UserName đã tồn tại");

            User user = new User()
            {
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName,
                Email = model.Email,
               
                UserInfor = new UserInfor()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Dob = model.Dob,
                }
            };

            if ((await _userManager.CreateAsync(user, model.Password)).Succeeded)
            {
                return new ApiSuccessResult<Guid>(user.Id,"Tạo mới thành công");
            }
            return new ApiErrorResult<Guid>("Tạo mới thất bại");
        }

        public async Task<ApiResult<string>> LoginCustomerAsync(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return new ApiErrorResult<string>("Tên đăng nhập không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, request.RememberMe, false);

            if (result.IsLockedOut == true)
                return new ApiErrorResult<string>("Tài khoản của bạn đã bị vô hiệu hóa");

            if (result.Succeeded == false)
                return new ApiErrorResult<string>("Mật khẩu không chính xác");

            if (await _context.CustomerInfors.FindAsync(user.Id) == null)
                return new ApiErrorResult<string>("Tài khoản không tồn tại");

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

        public async Task<ApiResult<bool>> CreateCustomerAsync(UserCreateRequest model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return new ApiErrorResult<bool>("Email đã tồn tại");
            if (await _userManager.FindByNameAsync(model.UserName) != null)
                return new ApiErrorResult<bool>("UserName đã tồn tại");

            User user = new User()
            {
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName,
                Email = model.Email,
                CustomerInfor = new CustomerInfor()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                }
            };

            if ((await _userManager.CreateAsync(user, model.Password)).Succeeded)
            {
                var roleCus = await _roleManager.FindByNameAsync("Customer");
                _context.UserRoles.Add(new IdentityUserRole<Guid>() { UserId = user.Id, RoleId = roleCus.Id });
                _context.SaveChanges();
                return new ApiSuccessResult<bool>(true,"Tạo mới thành công");
            }
            return new ApiErrorResult<bool>("Tạo mới thất bại");
        }

        public async Task<ApiResult<bool>> UpdateStaffAsync(UserUpdateRequest model)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == model.Email && x.Id != model.Id))
            {
                return new ApiErrorResult<bool>("Emai đã tồn tại");
            }

            var user = await _userManager.FindByIdAsync(model.Id.ToString());

            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            if (model.Status == Status.Active)
                user.LockoutEnabled = false;
            else
            {
                await _userManager.SetLockoutEnabledAsync(user, true);
                await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddYears(1000));
            }

            var userInfor = new UserInfor()
            {
                Id = user.Id,
                Dob = model.Dob,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var result = await _userManager.UpdateAsync(user);
            _context.UserInfors.Update(userInfor);
            _context.SaveChanges();
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>(true,"Cập nhật thành công");
            }
            else
                return new ApiErrorResult<bool>("Cập nhật không thành công");
        }

        public async Task<ApiResult<bool>> UpdateCustomerAsync(UserUpdateRequest model)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == model.Email && x.Id != model.Id))
            {
                return new ApiErrorResult<bool>("Emai đã tồn tại");
            }

            var user = await _userManager.FindByIdAsync(model.Id.ToString());

            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            if (model.Status == Status.Active)
                user.LockoutEnabled = false;
            else
            {
                await _userManager.SetLockoutEnabledAsync(user, true);
                await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddYears(1000));
            }

            var customerInfor = new CustomerInfor()
            {
                Id = user.Id,
                Dob = model.Dob,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var result = await _userManager.UpdateAsync(user);
            _context.CustomerInfors.Update(customerInfor);

            _context.SaveChanges();
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>(true);
            }
            else
                return new ApiErrorResult<bool>("Cập nhật không thành công");
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return new ApiErrorResult<bool>("Không tìm thấy người dùng");
            else
            {
                Guid currentUserId = GetUserId();
                if (id == currentUserId)
                    return new ApiErrorResult<bool>("Không thể tự xóa chính mình");
                else
                {
                    if (_context.Reservations.Where(x => x.CustomerId == id).Count() != 0)
                    {
                        await _userManager.SetLockoutEnabledAsync(user, true);
                        await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddYears(100));
                        return new ApiSuccessResult<bool>(true);
                    }
                    else
                    {
                        try
                        {
                            await _userManager.DeleteAsync(user);
                            return new ApiSuccessResult<bool>(true,"Xóa thành công");
                        }
                        catch (DbUpdateException e)
                        {
                            return new ApiErrorResult<bool>("Xảy ra lỗi trong quá trình xóa");
                        }
                    }
                }
            }
        }

        public async Task<ApiResult<bool>> ChangePasswordAsync(ChangePWRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                //Check confirm password
                return new ApiErrorResult<bool>("Tên đăng nhập không tồn tại");
            }
            else
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, request.OldPassword, false);
                if (result.Succeeded == false)
                    return new ApiErrorResult<bool>("Mật khẩu không chính xác");
                else
                {
                    await _userManager.RemovePasswordAsync(user);
                    await _userManager.AddPasswordAsync(user, request.NewPassword);
                    return new ApiSuccessResult<bool>(true,"Đổi mật khẩu thành công");
                }
            }
        }

        public async Task<ApiResult<bool>> RoleAssignAsync(RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }

            // check role available
            List<Guid> roles = request.Roles.Select(x => Guid.Parse(x.Id)).ToList();
            if (!CheckRoles(roles))
            {
                return new ApiErrorResult<bool>("Yêu cầu không hợp lệ");
            }

            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Id).ToList();
            foreach (var roleId in removedRoles)
            {
                var userRoles = _context.UserRoles.Where(x => x.RoleId.ToString() == roleId && x.UserId == user.Id);
                if (userRoles.Count() != 0)
                {
                    _context.UserRoles.RemoveRange(userRoles);
                }
            }

            var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Id).ToList();
            foreach (var roleId in addedRoles)
            {
                if (_context.UserRoles.Where(x => x.RoleId.ToString() == roleId && x.UserId == user.Id).Count() == 0)
                {
                    _context.UserRoles.Add(new IdentityUserRole<Guid>() { UserId = user.Id, RoleId = new Guid(roleId) });
                }
            }

            var userToken = _context.UserTokens.Find(request.UserId);
            if(userToken != null)
                _context.UserTokens.Remove(userToken);
            
            _context.SaveChanges();
            return new ApiSuccessResult<bool>(true,"Gián quyền thành công");
        }

        public async Task<ApiResult<UserVMD>> GetUserByIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var userInfor =await _context.UserInfors.FindAsync(id);

            if (user == null || userInfor == null)
            {
                return new ApiErrorResult<UserVMD>("Người dùng không tồn tại");
            }
            

            var userVMD = new UserVMD()
            {
                Dob = userInfor.Dob,
                FirstName = userInfor.FirstName,
                LastName = userInfor.LastName,
                Email = user.Email,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                Status = user.LockoutEnabled ? Status.InActive : Status.Active,
                UserName = user.UserName
               
            };
            userVMD.Roles = (List<string>)await _userManager.GetRolesAsync(user);

            return new ApiSuccessResult<UserVMD>(userVMD);
        }

        public async Task<ApiResult<UserVMD>> GetCustomerByIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var customerInfor = await _context.CustomerInfors.FindAsync(id);

            if (user == null || customerInfor==null)
            {
                return new ApiErrorResult<UserVMD>("Người dùng không tồn tại");
            }
           

            var userVMD = new UserVMD()
            {
                Dob = customerInfor.Dob,
                FirstName = customerInfor.FirstName,
                LastName = customerInfor.LastName,
                Email = user.Email,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                Status = user.LockoutEnabled ? Status.InActive : Status.Active,
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

            if (!string.IsNullOrWhiteSpace(request.RoleId))
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
                    Status = x.u.LockoutEnabled ? Status.InActive : Status.Active
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
            for (int i = 0; i < roles.Count; i++)
            {
                if (role == roles[i])
                    return true;
            }
            return false;
        }

        public Guid GetUserId()
        {
            var claimsIdentity = _accessor.HttpContext.User.Identity as ClaimsIdentity;
            string id = claimsIdentity.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault();
            return new Guid(id);
        }

        public ApiResult<bool> CheckToken(Guid userId,string token)
        {
            if (( _context.UserTokens.Where(x => x.UserId == userId && x.Value == token).Count()) > 0)
            {
                return new ApiSuccessResult<bool>(true,"Thành công");
            }
            else
                return new ApiErrorResult<bool>("Không hợp lệ");
        }
        public async Task<ApiResult<bool>> ForgotPasswordAsync(string mail)
        {
            var user = await _userManager.FindByEmailAsync(mail);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var schema = _configuration["AdminServer"];
                var url = $"{schema}/login/ResetPassword?Email={mail}&Token={token}";
                //var result = await VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, _userManager.ResetPasswordTokenPurpose, token));

                string message = string.Format("<p>Nhấn vào đây để khôi phục mật khẩu</p><a href = \"{0}\" >Link </a>", url);
                await _mailServive.SendEmailAsync(mail, "Khôi phục mật khẩu", message);
                return new ApiSuccessResult<bool>(true);

            }
            else
                return new ApiErrorResult<bool>("Email không tồn tại");

        }
        public async Task<ApiResult<bool>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user!=null)
            {
               request.Token = request.Token.Replace(" ", "+");
               var res =await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
                if (res.Succeeded)
                    return new ApiSuccessResult<bool>(true,"đổi mật khẩu thành công");
                else
                    return new ApiErrorResult<bool>("Đổi mật khẩu thất bại");
            }
            return new ApiErrorResult<bool>("Không tìm thấy người dùng");
        }
    }
}