using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.UserLogin
{
    internal class CheckUserLoginValidator : AbstractValidator<CheckUserLogin>
    {
        public CheckUserLoginValidator()
        {
            RuleFor(x => x.PhoneNumber).PhoneNumber();
            RuleFor(x => x.Password).Password();

        }
    }
}
