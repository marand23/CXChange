namespace CXChange.Modules.UserAccess.Application.Commands.UserLogin
{
    public record CheckUserLogin
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
