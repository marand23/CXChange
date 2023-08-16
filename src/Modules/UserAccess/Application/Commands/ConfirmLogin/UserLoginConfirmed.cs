namespace CXChange.Modules.UserAccess.Application.Commands.ConfirmLogin
{
    public record UserLoginConfirmed
    {
        public string Token { get; set; }
    }
}
