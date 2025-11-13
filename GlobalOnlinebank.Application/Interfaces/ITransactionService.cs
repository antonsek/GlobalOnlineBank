using GlobalOnlinebank.Domain.Entities;
using GlobalOnlinebank.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalOnlinebank.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<TransferResponseDto> ExecuteAsync(TransferRequestDto request, CancellationToken ct);

        Task<Transaction> GetByIdAsync(long id, CancellationToken ct = default);
        Task<List<Transaction>> GetFromCurrentMonthAsync(CancellationToken ct = default);
        Task<List<Transaction>> GetFromCurrentQuarterAsync(CancellationToken ct = default);
        Task<List<Transaction>> GetByPeriodAsync(DateTime from, DateTime to, CancellationToken ct = default);

    }
}
