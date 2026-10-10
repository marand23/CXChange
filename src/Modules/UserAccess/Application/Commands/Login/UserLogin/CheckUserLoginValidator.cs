using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.Login.UserLogin
{
    public class CheckUserLoginValidator : AbstractValidator<CheckUserLogin>
    {
        public CheckUserLoginValidator()
        {
            RuleFor(x => x.PhoneNumber).PhoneNumber();
            RuleFor(x => x.Password).Password();

        }
    }
}
