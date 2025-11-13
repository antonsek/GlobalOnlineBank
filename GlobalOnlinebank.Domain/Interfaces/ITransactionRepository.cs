using GlobalOnlinebank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalOnlinebank.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction?> GetByIdAsync(long id, CancellationToken ct = default);

        Task<List<Transaction>> GetByPeriodAsync(DateTime from, DateTime to, CancellationToken ct = default);
        Task<Transaction> CreateAsync(Transaction transaction, CancellationToken ct = default);
    }
}
