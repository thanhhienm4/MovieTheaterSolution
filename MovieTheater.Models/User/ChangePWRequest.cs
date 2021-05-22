using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.User
{
    public class ChangePWRequest
    {
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        public string OldPassword { get; set; }

        [Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }

        [Display(Name = "Nhập lại mật khẩu mới")]
        public string NewConfirmPassword { get; set; }
    }

    public class ChangePWRequestValidator : AbstractValidator<ChangePWRequest>
    {
        public ChangePWRequestValidator()
        {
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Mật khẩu không được để trống").Matches("^(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$")
               .WithMessage("Mật khẩu phải bao gồm chữ hoa , chữ thường, số, từ  8 đến 15 kí tự");
            RuleFor(x => x.NewConfirmPassword).NotEmpty().WithMessage("Vui lòng nhập lại mật khẩu");
            RuleFor(x => x.NewPassword).Equal(x => x.NewConfirmPassword).When(customer => !String.IsNullOrWhiteSpace(customer.NewPassword)).WithMessage("Mật khẩu không khớp");
        }
    }
}