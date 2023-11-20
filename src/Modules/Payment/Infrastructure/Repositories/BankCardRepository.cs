using CXChange.Modules.Payment.Domain.BankCard;
using CXChange.Modules.Payment.Domain.Interfaces;
using CXChange.Modules.Payment.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CXChange.Modules.Payment.Infrastructure.Repositories
{
    internal class BankCardRepository : IBankCardRepository
    {
        PaymentDbContext _context;
        public BankCardRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task AddBankCardAsync(BankCard bankCard)
        {
            await _context.AddAsync(bankCard);
        }

        public async Task<BankCard> GetBankCardByCardNumberAsync(string cardNumber)
        {
            return await _context.BankCards.FirstOrDefaultAsync(x => x.CardNumber.Address == cardNumber);
        }
        public async Task<BankCard> GetUserBankCardByCardNumber(string cardNumber, Guid userId)
        {
            return await _context.BankCards.FirstOrDefaultAsync(x => x.CardNumber.Address == cardNumber && x.UserId == userId);
        }
        public async Task<List<BankCard>> GetUserBankCardsAsync(Guid userId)
        {
            return await _context.BankCards.Where(x => x.UserId == userId).ToListAsync();

        }

        public async Task<bool> IsRepeatedBankCardAsync(string cardNubmer)
        {
            return await _context.BankCards.AnyAsync(x => x.CardNumber.Address == cardNubmer);
        }
    }
}
