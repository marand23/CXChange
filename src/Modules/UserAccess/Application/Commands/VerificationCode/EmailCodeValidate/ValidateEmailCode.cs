namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.EmailCodeValidate
{
    public record ValidateEmailCode
    {

        public string Code { get; init; }
        public string CodeType { get; init; }
        public string UserId { get; init; }

    }
}
