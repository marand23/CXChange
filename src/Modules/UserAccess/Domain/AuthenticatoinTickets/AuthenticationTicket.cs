using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.UserAccess.Domain.AuthenticatoinTickets
{
    public class AuthenticationTicket : Entity<string>, IAggregateRoot
    {
        private string Id { get; set; }
        private string UserId { get; set; }
        private byte[] Value { get; set; }
        private string DeviceId { get; set; }
        private string RemoteIpAddress { get; set; }
        private string OS { get; set; }
        private string UserAgentFamily { get; set; }
        private string UserAgenetVersion { get; set; }
        private DateTimeOffset? LastActivity { get; set; }
        private DateTimeOffset? Expires { get; set; }
    }
}
