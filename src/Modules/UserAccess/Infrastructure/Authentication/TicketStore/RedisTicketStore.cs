using Microsoft.AspNetCore.Authentication;

namespace CXChange.Modules.UserAccess.Infrastructure.Authentication.TicketStore
{
    internal class RedisTicketStore : ICustomTicketStore
    {
        public Task RemoveAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task RenewAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationTicket> RetrieveAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task<string> StoreAsync(AuthenticationTicket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
