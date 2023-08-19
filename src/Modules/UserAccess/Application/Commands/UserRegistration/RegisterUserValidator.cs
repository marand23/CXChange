
using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.UserRegistration
{
    public class RegisterUserValidator : AbstractValidator<RegisterUser>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.PhoneNumber).PhoneNumber();
            RuleFor(x => x.Password).Password();
        }
    }
}
