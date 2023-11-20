using CXChange.Modules.Payment.Domain.Interfaces;
using CXChange.Modules.Payment.Domain.Shaba;
using CXChange.Modules.Payment.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CXChange.Modules.Payment.Infrastructure.Repositories
{
    internal class ShabaRepository : IShabaRepository
    {
        PaymentDbContext _context;
        public ShabaRepository(PaymentDbContext context)
        {
            _context = context;
        }
        public async Task AddShabaAsync(Shaba shaba)
        {
            await _context.AddAsync(shaba);
        }

        public async Task<Shaba> GetShabaByNumberAsync(string shabaNumber)
        {
            return await _context.Shabas.FirstOrDefaultAsync(x => x.ShabaNumber.Address == shabaNumber);
        }

        public async Task<List<Shaba>> GetUserShabasAsync(Guid userId)
        {
            return await _context.Shabas.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<bool> IsRepeatedShabaAsync(string shabaNubmer)
        {
            return await _context.Shabas.AnyAsync(x => x.ShabaNumber.Address == shabaNubmer);
        }
    }
}
