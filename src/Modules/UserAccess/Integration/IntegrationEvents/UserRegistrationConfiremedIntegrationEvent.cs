namespace CXChange.Modules.UserAccess.Integration.IntegrationEvents
{
    public record UserRegistrationConfiremedIntegrationEvent
    {
        string PhoneNumber { get; set; }
    }
}
