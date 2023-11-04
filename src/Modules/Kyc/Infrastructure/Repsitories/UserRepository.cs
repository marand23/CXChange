using CXChange.Modules.Kyc.Domain.Interfaces.Repositories;
using CXChange.Modules.Kyc.Domain.User;
using CXChange.Modules.Kyc.Infrastructure.DataAccess;
using CXChange.Modules.Kyc.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

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
            return User.CreateFrom(new UserSnapShot
            {
                BirthDate = user.BirthDate,
                NationalId = user.NationlId,
                SurName = user.SurName,
                Name = user.Name,
                Id = user.Id,
                KycStatus = user.kycStatus,



            });

        }
        public async Task<bool> IsNationalIdUnique(string nationalId)
        {
            var id = await _context.Users.FirstOrDefaultAsync(x => x.NationlId == nationalId);
            return id == null;

        }
        public async Task Add(User user)
        {
            UserDataModel userModel = new UserDataModel();
            Map(userModel, user.GetSnapshot());
            _context.Users.Add(userModel);
        }
        public async Task UpdateUser(User user)
        {
            var snapshot = user.GetSnapshot();
            UserDataModel userData = await _context.Users.FindAsync(snapshot.Id);
            Map(userData, snapshot);
        }
        private void Map(UserDataModel userData, UserSnapShot snapshot)
        {
            userData.Id = snapshot.Id;
            userData.BirthDate = snapshot.BirthDate;
            userData.Name = snapshot.Name;
            userData.SurName = snapshot.SurName;
            userData.NationlId = snapshot.NationalId;
            userData.kycStatus = snapshot.KycStatus;
        }

    }
}
