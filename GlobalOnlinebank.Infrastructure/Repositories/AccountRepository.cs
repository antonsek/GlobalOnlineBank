using GlobalOnlinebank.Domain.Entities;
using GlobalOnlinebank.Domain.Interfaces;
using GlobalOnlinebank.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalOnlinebank.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {

        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Account> GetByIdAsync(long id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        public async Task<Account> AddAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task DeleteAsync(long id)
        {
            var account = await GetByIdAsync(id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task UpdateAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task<Account> GetByAccNumberAsync(string accNumber)
        {
            return await _context.Accounts.FindAsync(accNumber);
        }

        public async Task<Tariff> GetTariff(long id)
        {
            return await _context.Tariffs.FindAsync(id);
        }

        public async Task<string> GetLastIban()
        {
            // Найти максимальный IBAN, начинающийся с KZ5000
            var lastIban = await _context.Accounts   
                .OrderByDescending(a => a.AccountNumber)
                .Select(a => a.AccountNumber)
                .FirstOrDefaultAsync();

            return lastIban;
        }
    }
}
