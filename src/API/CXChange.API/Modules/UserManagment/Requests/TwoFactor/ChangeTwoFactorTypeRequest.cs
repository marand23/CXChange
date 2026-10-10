namespace CXChange.API.Modules.UserManagment.Requests.TwoFactor
{
    public class ChangeTwoFactorTypeRequest
    {

        public string Token { get; set; }
        public int NewTwoFactorType { get; set; }
    }
}
