namespace CXChange.Modules.Kyc.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User.User> FindUserById(Guid id);
        Task UpdateUser(User.User user);
        Task Add(User.User user);
        Task<bool> IsNationalIdUnique(string nationalId);

    }
}
