using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using CXChange.Modules.UserAccess.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CXChange.Modules.UserAccess.Infrastructure.Repositories
{
    internal class TicketRepository : ITicketRepository
    {
        private UserManagementDbContext _context;


        public TicketRepository(UserManagementDbContext context)
        {

            _context = context;



        }
        public async Task RemoveAsync(string key)
        {
            var ticket = await _context.AuthenticationTickets.SingleOrDefaultAsync(x => x.Id == "fs");

        }

        public async Task RemoveAllAsync(Guid UserId)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const int stringLength = 5;

            // Initialize a random number generator
            Random random = new Random();

            // Create a StringBuilder to build the random string
            StringBuilder randomString = new StringBuilder(stringLength);

            // Generate the random string
            for (int i = 0; i < stringLength; i++)
            {
                int index = random.Next(chars.Length);
                randomString.Append(chars[index]);
            }

            Console.WriteLine("this is test");
            using (var trnsaction = _context.Database.BeginTransaction(isolationLevel: System.Data.IsolationLevel.ReadCommitted))
            {
                //var t = _context.AuthenticationTickets.Where(x => x.UserId == "6d8c9051-0532-4888-8e57-da8e6e4616bf").FirstOrDefault();
                //t.DeviceId = randomString.ToString();
                //_context.AuthenticationTickets.Add(new AuthenticationTickets
                //{
                //    DeviceId = "",
                //    Expires = DateTime.UtcNow,
                //    Id = UserId,
                //    LastActivity = DateTime.UtcNow,
                //    OS = "",
                //    RemoteIpAddress = "",
                //    UserAgenetVersion = "1.0",
                //    UserAgentFamily = "nhj",
                //    UserId = "5895df32-dccd-4f57-81b0-dc99e7f31bf4",
                //    Value = new byte[8]
                //});
                Thread.Sleep(5000);
                _context.SaveChanges();

                trnsaction.Commit();
            }


            //  await _context.AuthenticationTickets.Where(c => c.UserId == UserId).ExecuteDeleteAsync();



        }
    }


}

