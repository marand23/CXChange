namespace CXChange.API.Modules.UserManagment.Requests.Password
{
    public record PasswordResetConfirmRequest(string Token, string NewPassword, string PhoneNumber);

}
