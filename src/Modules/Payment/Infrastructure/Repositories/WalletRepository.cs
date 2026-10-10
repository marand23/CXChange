using CXChange.Modules.Payment.Domain.Interfaces;
using CXChange.Modules.Payment.Domain.Wallet;
using CXChange.Modules.Payment.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CXChange.Modules.Payment.Infrastructure.Repositories
{
    internal class WalletRepository : IWalletRepository
    {
        PaymentDbContext _context;
        public WalletRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Wallet wallet)
        {
            await _context.AddAsync(wallet);

        }



        public async Task<Wallet> GetWalletByUserIdAsync(Guid userId, string symbol)
        {


            var query = from c in _context.CurrencyConfigs
                        join w in _context.Wallets
                            on c.Id equals w.CurrencyId
                        where w.UserId == userId
                        && c.Currency.Symbol == symbol
                        select w.MapCurrency(c.Currency.Name, c.Currency.Precision);
            var result = await query.FirstOrDefaultAsync();
            return result;
        }

        public async Task Clear()
        {
            _context.ChangeTracker.Clear();

        }

        public async Task<Wallet> GetWalletById(Guid id, string symbol)
        {

            var query = from c in _context.CurrencyConfigs
                        join w in _context.Wallets
                            on c.Id equals w.CurrencyId
                        where w.Id == id
                        && c.Currency.Symbol == symbol
                        select w.MapCurrency(c.Currency.Name, c.Currency.Precision);
            var result = await query.FirstOrDefaultAsync();
            return result;
        }
    }
}
