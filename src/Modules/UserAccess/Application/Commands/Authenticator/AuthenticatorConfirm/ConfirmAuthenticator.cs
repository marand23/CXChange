using CXChange.BuildingBlocks.Application.Command;
using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.Authenticator.AuthenticatorConfirm
{
    public record ConfirmAuthenticator : IValidatable, IPersistable, ICommandBase
    {
        public string UserId { get; init; }
        public string AuthToken { get; init; }
        public string CodeToken { get; init; }
    }
}
