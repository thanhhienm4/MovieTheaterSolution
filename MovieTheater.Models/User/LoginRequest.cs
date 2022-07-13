﻿using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.User
{
    public class LoginRequest
    {
        [Display(Name = "Tên đăng nhập")]
        public string Email { get; set; }

        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Nhớ mật khẩu")]
        public bool RememberMe { get; set; }

        public string RedirectUrl { get; set; }
    }

    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Vui lòng nhập tên đăng nhập");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Vui lòng nhập mật khẩu");
        }
    }
}