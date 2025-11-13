using GlobalOnlinebank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalOnlinebank.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> GetByIdAsync(long id);
        Task<IEnumerable<Account>> GetAllAsync();
        Task<Account> AddAsync(Account account);
        Task UpdateAsync(Account account);
        Task DeleteAsync(long id);
        Task<Account> GetByAccNumberAsync(string accNumber);

        Task<string> GetLastIban();
    }
}
