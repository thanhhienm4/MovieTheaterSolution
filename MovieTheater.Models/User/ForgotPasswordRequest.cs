using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.User
{
    public class ForgotPwRequest
    {
        [Display(Name = "Email khôi phục mật khẩu")]
        public string Email { get; set; }
    }

    public class ForgotPwValidator : AbstractValidator<ForgotPwRequest>
    {
        public ForgotPwValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Định dạng mail không hợp lệ");
        }
    }
}