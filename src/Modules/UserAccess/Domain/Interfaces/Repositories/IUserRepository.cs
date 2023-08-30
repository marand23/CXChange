namespace CXChange.Modules.UserAccess.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task CreateUser(User.User user);
        Task Update(User.User user);
        Task<User.User> GetUserById(string id);
        Task RemoveTicket(User.User user);
        Task<User.User> GetUserByPhoneNumber(string phoneNumber);
        Task<byte[]> GetUserSecurityToken(User.User user);
        Task DeleteUser(string phonNumber);
        Task CheckUserPassword(string id, string Password);
        Task<User.User> CheckSignIn(string PhoneNumber, string Password);
        Task SignIn(User.User user);
        Task<string> GenerateUserToken(User.User user, string tokenProvider, string purpose);
        Task<bool> ValidateUserToken(User.User user, string token, string tokenProvider, string purpose);
        Task<string> GeneratePasswordResetToken(User.User user);

        Task ResetPassword(User.User user, string token, string newPassword);
        Task ChangePassword(User.User user, string oldPassword, string newPassword);
        Task<bool> CheckPassword(User.User user, string password);
        Task ResetAuthenticatorKey(User.User user);
        Task<string> GetAuthenticatorKey(User.User user);

    }
}
