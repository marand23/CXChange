using CXChange.Modules.UserAccess.Domain.AuthenticatoinTickets;

namespace CXChange.Modules.UserAccess.Domain.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        Task<string> StoreAsync(AuthenticationTicket ticket);
        Task RenewAsync(string key, AuthenticationTicket ticket);
        Task<AuthenticationTicket> RetrieveAsync(string key);
        Task RemoveAsync(string key);

    }
}
