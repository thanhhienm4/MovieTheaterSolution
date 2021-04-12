using FluentValidation;

namespace MovieTheater.Models.User
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Vui lòng nhập tên đăng nhập");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Vui lòng nhập mật khẩu");
        }
    }
}