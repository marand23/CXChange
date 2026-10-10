using CXChange.Modules.Payment.Domain.Deposit;
using CXChange.Modules.Payment.Domain.Interfaces;
using CXChange.Modules.Payment.Infrastructure.DataAccess;

namespace CXChange.Modules.Payment.Infrastructure.Repositories
{

    internal class DepositRepository : IDepositRepository
    {
        PaymentDbContext _context;
        public DepositRepository(PaymentDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Deposit deposit)
        {
            await _context.AddAsync(deposit);
        }

        public async Task<Deposit> GetDepositById(Guid id)
        {
            return await _context.Deposits.FindAsync(id);
        }
    }
}
