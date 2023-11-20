namespace CXChange.Modules.Payment.Domain.Interfaces
{
    public interface IShabaRepository
    {
        Task<Shaba.Shaba> GetShabaByNumberAsync(string shabaNumber);
        Task<List<Shaba.Shaba>> GetUserShabasAsync(Guid userId);
        Task<bool> IsRepeatedShabaAsync(string cardNubmer);
        Task AddShabaAsync(Shaba.Shaba bankCard);
    }
}
