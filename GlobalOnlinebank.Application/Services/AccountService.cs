using GlobalOnlinebank.Application.DTOs;
using GlobalOnlinebank.Application.Extensions;
using GlobalOnlinebank.Application.Interfaces;
using GlobalOnlinebank.Domain.Entities;
using GlobalOnlinebank.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalOnlinebank.Application.Services
{
    public class AccountService: IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Task<AccountDto> CreateAsync(CreateAccountDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AccountDto>> GetAllAsync()
        {
            var accounts = await _accountRepository.GetAllAsync();
            return accounts.Select(p => p.ToDto());
        }

        public async Task<AccountDto> GetByIdAsync(long id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            return account.ToDto();
        }

        public Task UpdateAsync(long id, UpdateAccountDto dto)
        {
            throw new NotImplementedException();
        }

    }
}
