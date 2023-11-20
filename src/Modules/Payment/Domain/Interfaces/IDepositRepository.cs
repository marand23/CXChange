namespace CXChange.Modules.Payment.Domain.Interfaces
{
    public interface IDepositRepository
    {
        Task AddAsync(Deposit.Deposit deposit);
        Task<Deposit.Deposit> GetDepositById(Guid id);
    }
}
