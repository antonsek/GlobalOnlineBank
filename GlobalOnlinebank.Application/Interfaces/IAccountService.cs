using GlobalOnlinebank.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalOnlinebank.Application.Interfaces
{
    public interface IAccountService
    {

        Task<AccountDto> GetByIdAsync(long id);
        Task<IEnumerable<AccountDto>> GetAllAsync();

        Task DepositBalance(long id, decimal amount, string currency);

        Task WithdrawBalance(long id, decimal amount, string currency);

        Task<AccountDto> CreateAsync(CreateAccountDto dto);
        Task UpdateAsync(long id, UpdateAccountDto dto);
        Task DeleteAsync(long id);

        Task<AccountDto> GetByAccNumberAsync(string id);

        Task<AccountDto> GetBonusAccount();

        Task<string> GenereateIban();

    }
}
