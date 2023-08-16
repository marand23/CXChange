namespace CXChange.API.Modules.UserManagment.Requests.UserLogin
{
    public record CheckLoginRequest
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
