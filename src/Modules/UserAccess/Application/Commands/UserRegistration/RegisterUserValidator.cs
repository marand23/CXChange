using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Application.Commands.UserRegistration
{
    public class RegisterUserValidator:AbstractValidator<RegisterUser>
    {
        public RegisterUserValidator() {
            RuleFor(x => x.PhoneNumber).MinimumLength(10).WithMessage("شماره تلفن صحیح نمیباشد ");
            RuleFor(x => x.Password).MinimumLength(10).WithMessage("رمز صحیح نمیباشذ");
        }
    }
}
