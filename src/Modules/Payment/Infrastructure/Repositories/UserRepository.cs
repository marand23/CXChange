using CXChange.Modules.Payment.Domain.Interfaces;
using CXChange.Modules.Payment.Domain.User;
using CXChange.Modules.Payment.Infrastructure.DataAccess;

namespace CXChange.Modules.Payment.Infrastructure.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly PaymentDbContext _context;
        public UserRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task<User> FindById(Guid id)
        {
            var userDto = await _context.Users.FindAsync(id);
            return userDto;
        }

        public async Task Update(User user)
        {

            // var userDto = await _context.Users.Update(user);

        }
        public async Task Add(User user)
        {
            _context.Users.Add(user);
        }


    }
}
