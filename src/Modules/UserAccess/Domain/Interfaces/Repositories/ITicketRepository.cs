namespace CXChange.Modules.UserAccess.Domain.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        Task RemoveAsync(string key);
        //   Task<string> StoreAsync(AuthenticationTicket ticket);
        //  Task RenewAsync(string key, AuthenticationTicket ticket);
        //Task<AuthenticationTicket> RetrieveAsync(string key);
        Task RemoveAllAsync(Guid UserId);

    }
}
