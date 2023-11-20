namespace CXChange.Modules.Payment.Domain.Interfaces
{
    public interface IWalletRepository
    {
        Task AddAsync(Wallet.Wallet wallet);

        Task<Wallet.Wallet> GetWalletAsync(Guid userId, string symbol);
    }
}
