using FluentValidation;
using MovieTheater.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.User
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }
        [Display(Name = "Tên Đăng nhập")]
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
        [Display(Name = "Trạng thái")]
        public Status Status { get; set; }
    }
    public class UserUpdateValidator : AbstractValidator<UserUpdateRequest>
    {
        public UserUpdateValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Tên không được để trống");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Họ không được để trống");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email không được để trống ").EmailAddress().WithMessage("Email không hợp lệ");
            RuleFor(x => x.Dob).LessThan(DateTime.Now.AddYears(-18)).WithMessage("Người dùng phải đủ 18 tuổi");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Số điện thoại không được để trống ").Matches("[0-9]{10}").WithMessage("Số điện thoại không hợp lệ");
        }
    }
}
