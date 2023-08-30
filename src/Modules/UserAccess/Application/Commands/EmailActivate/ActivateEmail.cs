namespace CXChange.Modules.UserAccess.Application.Commands.EmailActivate
{
    public record ActivateEmail
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
