namespace CXChange.Modules.UserAccess.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task CreateUser(User.User user);
        Task UpdateUser(User.User user);
        Task<User.User> GetUserById(string id);
        Task RemoveTicket(User.User user);
        Task<User.User> GetUserByPhoneNumber(string phoneNumber);
        Task<byte[]> GetUserSecurityToken(User.User user);
        Task DeleteUser(string phonNumber);
        Task ConfirmPhoneNumber(string PhoneNumber);
        Task CheckUserPassword(string PhoneNumber, string Password);
        Task<User.User> CheckSignIn(string PhoneNumber, string Password);
        Task SignIn(User.User user);
        Task<string> GenerateUserToken(User.User user);
        Task<bool> ValidateUserToken(User.User user, string token);
    }
}
