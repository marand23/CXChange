namespace CXChange.API.Modules.UserManagment.Requests.UserLogin
{
    public record CheckLoginRequest
    {
        public string PhoneNumber { get; init; }
        public string Password { get; init; }

    }
}
