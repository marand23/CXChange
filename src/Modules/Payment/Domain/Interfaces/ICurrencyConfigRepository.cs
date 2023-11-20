using CXChange.Modules.Payment.Domain.Currency;

namespace CXChange.Modules.Payment.Domain.Interfaces
{
    public interface ICurrencyConfigRepository
    {

        public Task<CurrencyConfig> GetCurrencyConfigAsync(int id);
        Task<CurrencyConfig> GetCurrencyConfigBySymbolAsync(string symbol);
    }
}
