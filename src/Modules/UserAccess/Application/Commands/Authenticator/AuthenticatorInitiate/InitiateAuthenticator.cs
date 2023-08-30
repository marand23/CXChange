using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.Authenticator.AuthenticatorInitiate
{
    public record InitiateAuthenticator : IPersistable
    {
        public string UserId { get; set; }
    }
}
