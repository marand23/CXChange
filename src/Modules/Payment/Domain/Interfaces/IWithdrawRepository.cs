namespace CXChange.Modules.Payment.Domain.Interfaces
{
    public interface IWithdrawRepository
    {
        Task AddAsync(Withdraw.Withdraw withdraw);
        Task<IEnumerable<Withdraw.Withdraw>> GetWithdrawsVerifiedByUser(string symbol);
        Task<IEnumerable<Withdraw.Withdraw>> GetSentWithdraws(string symbol);

    }
}
