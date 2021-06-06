using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.User
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string Token { get; set; }


        [Display(Name = "Mật khẩu")]
        public String Password { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        public String ConfirmPassword { get; set; }
        public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
        {
            public ResetPasswordValidator()
            {
              
                RuleFor(x => x.Password).NotEmpty().WithMessage("Mật khẩu không được để trống").Matches("^(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$")
                    .WithMessage("Mật khẩu phải bao gồm chữ hoa , chữ thường, số, từ  8 đến 15 kí tự");
                RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Vui lòng nhập lại mật khẩu");
                RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).When(customer => !String.IsNullOrWhiteSpace(customer.Password)).WithMessage("Mật khẩu không khớp");
            }
        }
    }
}
