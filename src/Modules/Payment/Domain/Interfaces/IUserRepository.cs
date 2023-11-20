namespace CXChange.Modules.Payment.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User.User> FindById(Guid id);
        Task Update(User.User user);
        Task Add(User.User user);
    }
}
