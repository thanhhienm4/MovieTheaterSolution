using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application
{
    public class UserService
    {
        private readonly MovieTheaterDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UserService(MovieTheaterDBContext context, UserManager<AppUser> userManager
            , SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
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
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role,string.Join(";",roles))

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
            } else
            {
                userToken.Value = token;
                _context.UserTokens.Update(userToken);
            }
            _context.SaveChanges();


            return new ApiSuccessResult<string>(token);
        }

        public async Task<ApiResultLite> CreateAsync(UserCreateVMD model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return new ApiErrorResultLite("Email đã tồn tại");
            if (await _userManager.FindByNameAsync(model.UserName) != null)
                return new ApiErrorResultLite("UserName đã tồn tại");

            var uid = Guid.NewGuid();
            while (await _userManager.FindByIdAsync(uid.ToString()) != null)
            {
                uid = Guid.NewGuid();
            }

            AppUser user = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName,
                Email = model.Email,
                
            };
            if((await _userManager.CreateAsync(user, model.Password)).Succeeded)
            {
                return new ApiSuccessResultLite();
            }
            return new ApiErrorResultLite("Tạo mới thất bại");

        }

        private bool CheckRoles(List<Guid> roles)
        {
            var listRole = _context.Roles.Select(x => x.Id).ToList();
            if(roles!=null)
            {
                if (listRole == null)
                    return false;
                foreach(var role in roles)
                { 
                    if(!CheckRole(role,listRole))
                    {
                        return false;
                    }
                }
                
            }
            return true;
        }
        private bool CheckRole(Guid role, List<Guid> roles)
        {
            for(int i=0;i<= 0;i++)
            {
                if (role == roles[i])
                    return true;
            }
            return false;
        }

       
    }
}
