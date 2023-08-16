namespace CXChange.Modules.UserAccess.Application.Commands.UserLogin
{
    public record UserLoginChecked
    {
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
    }
}
