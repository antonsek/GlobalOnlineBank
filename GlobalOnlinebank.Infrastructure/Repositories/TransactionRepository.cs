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
    public class TransactionRepository: ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Transaction?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            return await _context.Transactions
            .Include(t => t.SenderAccount)
            .Include(t => t.ReceiverAccount)
            .FirstOrDefaultAsync(t => t.Id == id, ct);
        }

        public async Task<List<Transaction>> GetByPeriodAsync(DateTime from, DateTime to, CancellationToken ct = default)
        {
            return await _context.Transactions
                .Where(t => t.CreatedAt >= from && t.CreatedAt <= to)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync(ct);
        }

        public async Task<Transaction> CreateAsync(Transaction transaction, CancellationToken ct = default)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync(ct);
            return transaction;
        }

    }
}
