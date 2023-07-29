namespace CXChange.Modules.UserAccess.Infrastructure.DataModel
{
    public class AuthenticationTickets
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public byte[] Value { get; set; }
        public string DeviceId { get; set; }
        public string RemoteIpAddress { get; set; }
        public string OS { get; set; }
        public string UserAgentFamily { get; set; }
        public string UserAgenetVersion { get; set; }
        public DateTimeOffset? LastActivity { get; set; }
        public DateTimeOffset? Expires { get; set; }
    }
}
