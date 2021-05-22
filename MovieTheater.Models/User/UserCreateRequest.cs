using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MovieTheater.Models.User
{
    public class UserCreateRequest
    {
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Display(Name = "Tên")]
        public string FirstName { get; set; }

        [Display(Name = "Họ")]
        public string LastName { get; set; }

        [Display(Name = "Ngày sinh")]
        public DateTime Dob { get; set; }

        public String Email { get; set; }

        [Display(Name = "Số điện thoại")]
        public String PhoneNumber { get; set; }

        [Display(Name = "Mật khẩu")]
        public String Password { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        public String ConfirmPassword { get; set; }
    }

    public class UserCreateValidator : AbstractValidator<UserCreateRequest>
    {
        public UserCreateValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Tên đăng nhập không được bỏ trống").Must(u => !u.Any(x => Char.IsWhiteSpace(x))).When(u => !string.IsNullOrWhiteSpace(u.UserName)).WithMessage("Tên đăng nhập không bao gồm khoảng trắng");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Tên không được bỏ trống");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Họ không được bỏ trống");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email không được bỏ trống ").EmailAddress().WithMessage("Email không đúng định dạng");
            RuleFor(x => x.Dob).LessThan(DateTime.Now.AddYears(-18)).WithMessage("Người dùng phải đủ 18 tuổi");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Số điện thoại không được để trống").Matches("^0[0-9]{9}$").WithMessage("Số điện thoại 10 số bắt đầu từ số 0");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Mật khẩu không được để trống").Matches("^(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$")
                .WithMessage("Mật khẩu phải bao gồm chữ hoa , chữ thường, số, từ  8 đến 15 kí tự");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Vui lòng nhập lại mật khẩu");
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).When(customer => !String.IsNullOrWhiteSpace(customer.Password)).WithMessage("Mật khẩu không khớp");
        }
    }
}