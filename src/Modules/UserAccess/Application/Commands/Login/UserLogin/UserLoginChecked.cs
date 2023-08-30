namespace CXChange.Modules.UserAccess.Application.Commands.Login.UserLogin
{
    public record UserLoginChecked
    {
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
    }
}
