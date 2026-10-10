namespace CXChange.Modules.Payment.Domain.Interfaces
{
    public interface IWalletRepository
    {
        Task AddAsync(Wallet.Wallet wallet);

        Task<Wallet.Wallet> GetWalletByUserIdAsync(Guid userId, string symbol);
        Task<Wallet.Wallet> GetWalletById(Guid id, string symbol);


    }
}
