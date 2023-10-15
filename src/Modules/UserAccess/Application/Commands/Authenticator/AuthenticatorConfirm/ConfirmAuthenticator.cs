using CXChange.BuildingBlocks.Application.Command;
using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.Authenticator.AuthenticatorConfirm
{
    public record ConfirmAuthenticator : IValidatable, IPersistable, ICommandBase, INeedsAuthentication
    {
        public string UserId { get; set; }
        public string AuthToken { get; init; }
        public string CodeToken { get; init; }
    }
}
