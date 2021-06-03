using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.User
{
    public class ForgotPasswordRequest
    {
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
