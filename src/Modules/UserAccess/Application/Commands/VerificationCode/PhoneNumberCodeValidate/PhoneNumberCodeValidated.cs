namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.CodeValidate
{
    public record PhoneNumberCodeValidated
    {
        //when code  validated a user token will be generated 
        //and other commands will verify it for taking the action 
        public string Token { get; set; }
    }
}
