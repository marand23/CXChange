using FluentValidation;

namespace CXChange.API.Modules.UserManagment
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.PhoneNumber).Length(11);

        }
    }
}
