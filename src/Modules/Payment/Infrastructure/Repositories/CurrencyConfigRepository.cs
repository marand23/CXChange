using CXChange.Modules.Payment.Domain.Currency;
using CXChange.Modules.Payment.Domain.Interfaces;
using CXChange.Modules.Payment.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CXChange.Modules.Payment.Infrastructure.Repositories
{
    internal class CurrencyConfigRepository : ICurrencyConfigRepository
    {
        public PaymentDbContext _context;
        public CurrencyConfigRepository(PaymentDbContext context)
        {
            _context = context;
        }
        public async Task<CurrencyConfig> GetCurrencyConfigAsync(int id)
        {
            return await _context.CurrencyConfigs.FindAsync(id);
        }
        public async Task<CurrencyConfig> GetCurrencyConfigBySymbolAsync(string symbol)
        {
            return await _context.CurrencyConfigs.FirstOrDefaultAsync(c => c.Currency.Symbol == symbol);
        }
    }
}
