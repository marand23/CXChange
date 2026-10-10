using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.Authenticator.AuthenticatorInitiate
{
    public record InitiateAuthenticator : IPersistable
    {
        public Guid UserId { get; set; }
    }
}
