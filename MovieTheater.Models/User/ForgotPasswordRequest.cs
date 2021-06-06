using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.User
{
    public class ForgotPasswordRequest
    {
        [Display(Name = "Email khôi phục mật khẩu")]
        public string Email { get; set; }
        
        
    }
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordRequest>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Định dạng mail không hợp lệ");
        }
    }
}
