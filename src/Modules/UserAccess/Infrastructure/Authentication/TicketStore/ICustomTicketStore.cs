using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Infrastructure.Authentication.TicketStore
{
    internal interface ICustomTicketStore
    {
        Task<string> StoreAsync(AuthenticationTicket ticket);
        Task<AuthenticationTicket> RetrieveAsync(string token);
        Task RemoveAsync(string key);
        Task RenewAsync(string key);
        Task RemoveAllAsync();
    }
}
