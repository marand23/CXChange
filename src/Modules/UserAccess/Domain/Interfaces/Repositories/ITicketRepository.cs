using CXChange.Modules.UserAccess.Domain.AuthenticatoinTickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
