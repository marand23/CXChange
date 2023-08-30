namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.CodeValidate
{
    public record ValidateCode
    {
        public string UserId { get; set; }
        public string Code { get; set; }
        public string CodeType { get; set; }
    }
}
