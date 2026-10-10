namespace CXChange.Modules.UserAccess.Application.Commands.AdministratorCommands.UserBan
{
    public record BanUser
    {
        public Guid UserId { get; init; }
    }
}
