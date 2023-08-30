namespace CXChange.API.Modules.UserManagment.Requests.Password
{
    public class PasswordResetConfirmRequest
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
        public string PhoneNumber { get; set; }
    }
}
