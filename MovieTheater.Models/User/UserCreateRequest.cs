
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.User
{
    public class UserCreateRequest
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public String Email { get; set; }

        public String PhoneNumber { get; set; }

        public String Password { get; set; }

        public String ConfirmPassword { get; set; }
    }
    public class UserCreateValidator : AbstractValidator<UserCreateRequest>
    {
        public UserCreateValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Tên không được bỏ trống");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Họ không được bỏ trống");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email không được bỏ trống ").EmailAddress().WithMessage("Email không đúng định dạng");
            RuleFor(x => x.Dob).LessThan(DateTime.Now.AddYears(-18)).WithMessage("Người dùng phải đủ 18 tuổi");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Số điện thoại khong được bỏ trống ").Matches("[0-9]{10}").WithMessage("Số điện thoại không chính xác");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Mật khẩu không được để trống").Matches("^(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$")
                .WithMessage("Mật khẩu phải bao gồm chữ hoa , chữ thường, số, từ  8 đến 15 kí tự");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Vui lòng nhập lại mật khẩu");
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).When(customer => !String.IsNullOrWhiteSpace(customer.Password)).WithMessage("Mật khẩu không khớp");
        }
    }
}
