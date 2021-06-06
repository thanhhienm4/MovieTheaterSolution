using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using MovieTheater.Api;
using MovieTheater.Models.User;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly UserApiClient _userApiClient;
        private readonly IConfiguration _configuration;

        public LoginController(UserApiClient userApiClient, IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("Token");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest request)
        {
            Response.Cookies.Delete("Token");
            if (ModelState.IsValid == false)
                return View();
            var respond = await _userApiClient.LoginStaffAsync(request);
            if (respond.IsSuccessed == false)
            {
                ModelState.AddModelError("", respond.Message);
                return View();
            }

            var userPrincipal = ValidateToken(respond.ResultObj);
            var authProperties = new AuthenticationProperties()
            {
                ExpiresUtc = DateTimeOffset.Now.AddMonths(1),
                IsPersistent = request.RememberMe,
            };

            Response.Cookies.Append("Token", respond.ResultObj, new CookieOptions() { Expires = DateTimeOffset.Now.AddMonths(1) });
            await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        userPrincipal,
                        authProperties);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);
            else
            {
                var response = await _userApiClient.ForgotPasswordAsync(model.Email);
                if (response.IsSuccessed == true)
                {

                    ViewBag.Message = "Đã gửi link xác nhận về địa chỉ mail của bạn";
                }
                else
                {
                    ViewBag.Message = response.Message;
                }
                return View(model);
            }


        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            else
            {
                var response = await _userApiClient.ResetPasswordAsync(request);
                if (response.IsSuccessed == true)
                {

                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ViewBag.Message = response.Message;
                }
                return View(request);
            }
        }

        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidAudience = _configuration["JWT:ValidAudience"],
                ValidIssuer = _configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]))
            };
            ClaimsPrincipal claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, tokenValidationParameters,
                                                                            out SecurityToken validatedToken);
            return claimsPrincipal;
        }
    }
}