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
            if (user == null) { return null; }
            return User.CreateFrom(new UserSnapShot
            {
                BirthDay = user.BirthDate,
                NationalId = user.NationlId,
                SurName = user.SurName,
                Name = user.Name,
                Id = user.Id,
                KycStatus = user.kycStatus,
                CreatedAt = user.CreatedAt,
                Version = user.Version,
                LandLineStatus = user.LandLineStatus,
                LandLine = user.LandLine,



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
            snapshot.Version++;
            UserDataModel userData = await _context.Users.FindAsync(snapshot.Id);
            Map(userData, snapshot);
        }
        private void Map(UserDataModel userData, UserSnapShot snapshot)
        {
            userData.Id = snapshot.Id;
            userData.BirthDate = snapshot.BirthDay;
            userData.Name = snapshot.Name;
            userData.SurName = snapshot.SurName;
            userData.NationlId = snapshot.NationalId;
            userData.kycStatus = snapshot.KycStatus;
            userData.CreatedAt = snapshot.CreatedAt;
            userData.Version = snapshot.Version;
            userData.LandLineStatus = snapshot.LandLineStatus;
            userData.LandLine = snapshot.LandLine;
        }

    }
}
