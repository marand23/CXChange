namespace CXChange.Modules.UserAccess.Application.Commands.EmailChange
{
    public record ChangeEmail
    {
        public Guid UserId { get; init; }
        public string Email { get; init; }
        public string Token { get; init; }
        public string Code { get; init; }
    }
}
