namespace CXChange.Modules.UserAccess.Application.Commands.Login.ConfirmLogin
{
    public record UserLoginConfirmed
    {
        public string Token { get; set; }
    }
}
