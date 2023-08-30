namespace CXChange.API.Modules.UserManagment.Requests.UserRegister
{
    public record ResendSmsForRegisterRequest
    {
        public string PhoneNumber { get; set; }
    }
}
