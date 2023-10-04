using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.CodeValidate
{
    public record ValidatePhoneNumberCode : IPersistable
    {

        public string Code { get; init; }
        public string CodeType { get; init; }
        public string UserId { get; init; }
        public string PhoneNumber { get; init; }
    }
}
