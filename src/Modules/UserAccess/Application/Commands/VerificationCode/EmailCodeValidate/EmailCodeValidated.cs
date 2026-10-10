namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.EmailCodeValidate
{
    public record EmailCodeValidated
    {
        public string Token { get; init; }
    }
}
