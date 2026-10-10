using CXChange.Modules.Payment.Domain.Interfaces;
using CXChange.Modules.Payment.Domain.Withdraw;
using CXChange.Modules.Payment.Domain.Withdraw.Enums;
using CXChange.Modules.Payment.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CXChange.Modules.Payment.Infrastructure.Repositories
{
    internal class WithdrawRepository : IWithdrawRepository
    {
        PaymentDbContext _context;
        public WithdrawRepository(PaymentDbContext context) { _context = context; }
        public async Task AddAsync(Withdraw withdraw)
        {
            await _context.Withdraws.AddAsync(withdraw);
        }
        public async Task<IEnumerable<Withdraw>> GetWithdrawsVerifiedByUser(string symbol)
        {
            return await _context.Withdraws.Where(x => x.Status == WithdrawStatus.ConfirmedByUser && x.Currency.Symbol == symbol).ToListAsync();
        }

        public async Task<IEnumerable<Withdraw>> GetSentWithdraws(string symbol)
        {
            return await _context.Withdraws.Where(x => x.Status == WithdrawStatus.Sent && x.Currency.Symbol == symbol).ToListAsync();
        }
    }
}
