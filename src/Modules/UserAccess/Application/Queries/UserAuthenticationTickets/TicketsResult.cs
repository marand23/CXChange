namespace CXChange.Modules.UserAccess.Application.Queries.UserAuthenticationTickets
{
    public record TicketsResult
    {
        public string DeviceId { get; set; }
        public string RemoteIpAdress { get; set; }
        public string OS { get; set; }
        public string UserAgentFamily { get; set; }
    }
}
