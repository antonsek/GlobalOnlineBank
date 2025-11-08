
using GlobalOnlinebank.Domain.Entities;
using GlobalOnlinebank.Domain.Interfaces;
using GlobalOnlinebank.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GlobalOnlinebank.Infrastructure.Repositories
{
    public class ContragentRepository : IContragentRepository
    {
        private readonly ApplicationDbContext _context;

        public ContragentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Contragent> GetByIdAsync(long id)
        {
            return await _context.Contragents.FindAsync(id);
        }

        public async Task<IEnumerable<Contragent>> GetAllAsync()
        {
            return await _context.Contragents.ToListAsync();
        }

        public async Task<Contragent> AddAsync(Contragent contragent)
        {
            await _context.Contragents.AddAsync(contragent);
            await _context.SaveChangesAsync();
            return contragent;
        }

        public async Task UpdateAsync(Contragent contragent)
        {
            _context.Contragents.Update(contragent);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var product = await GetByIdAsync(id);
            _context.Contragents.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}