namespace CXChange.API.Modules.UserManagment.Requests.TwoFactor
{
    public class ChangeTwoFactorTypeRequest
    {

        public string Token { get; set; }
        public string NewTwoFactorType { get; set; }
    }
}
