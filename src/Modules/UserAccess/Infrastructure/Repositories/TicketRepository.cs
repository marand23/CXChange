using CXChange.Modules.UserAccess.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CXChange.Modules.UserAccess.Infrastructure.Repositories
{
    internal class TicketRepository /*: ITicketRepository*/
    {
        private readonly UserManagementDbContext _context;
        public TicketRepository(UserManagementDbContext context)
        {
            _context = context;
        }
        public async Task RemoveAsync(string key)
        {
            var ticket = await _context.AuthenticationTickets.SingleOrDefaultAsync(x => x.Id == key);
            if (ticket != null)
            {
                _context.AuthenticationTickets.Remove(ticket);

            }
        }

        public async Task RenewAsync(string key)
        {

            var authenticationTicket = await _context.AuthenticationTickets.FindAsync(key);
            if (authenticationTicket != null)
            {
                //  authenticationTicket.Value = SerializeToBytes(ticket);
                authenticationTicket.LastActivity = DateTimeOffset.UtcNow;
                //authenticationTicket.Expires = ticket.Properties.ExpiresUtc;

            }

        }
    }

    //public Task<AuthenticationTicket> RetrieveAsync(string key)
    //{
    //    throw new NotImplementedException();
    //}

    //public Task<string> StoreAsync(AuthenticationTicket ticket)
    //{
    //    throw new NotImplementedException();
    //}
}

