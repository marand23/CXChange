using CXChange.Modules.Kyc.Domain.Interfaces.Repositories;
using CXChange.Modules.Kyc.Domain.User;
using CXChange.Modules.Kyc.Infrastructure.DataAccess;

namespace CXChange.Modules.Kyc.Infrastructure.Repsitories
{
    internal class UserRepository : IUserRepository
    {
        KycDbContext _context;
        public UserRepository(KycDbContext context)
        {
            _context = context;
        }
        public async Task<User> FindUserById(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) { return null; }
            return user;

        }
        public async Task<bool> IsNationalIdUnique(string nationalId)
        {
            //  var id = await _context.Users.FirstOrDefaultAsync(x =>  == nationalId);
            //return id == null;
            throw new NotImplementedException();

        }
        public async Task Add(User user)
        {

            _context.Users.Add(user);
        }
        public async Task UpdateUser(User user)
        {


        }


    }
}
