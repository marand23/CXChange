using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.AuthenticatorDeactivate
{
    public record DeactivateAuthenticator : IPersistable, IValidatable
    {
        public Guid UserId { get; init; }
        public string Token { get; init; }
    }
}
