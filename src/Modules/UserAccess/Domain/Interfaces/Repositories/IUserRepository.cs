namespace CXChange.Modules.UserAccess.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task CreateUser(User.User user);
        Task UpdateUser(User.User user);
        Task<User.User> GetUserById(string id);
        Task RemoveTicket(User.User user);
        Task GetUserByPhoneNumber(string phoneNumber);
        Task<byte[]> GetUserSecurityToken(User.User user);
    }
}
