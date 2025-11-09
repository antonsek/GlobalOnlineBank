using GlobalOnlinebank.Application.DTOs;
using GlobalOnlinebank.Application.Extensions;
using GlobalOnlinebank.Application.Interfaces;
using GlobalOnlinebank.Domain.Entities;
using GlobalOnlinebank.Domain.Interfaces;
using Shared;
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

        public async  Task<AccountDto> CreateAsync(CreateAccountDto dto)
        {
            await _accountRepository.AddAsync(new Account(
                dto.ContragentID,
                dto.AccountNumber,
                dto.Currency,
                dto.AccountType
                ));

            var account = await _accountRepository.GetByAccNumberAsync(dto.AccountNumber);
            return account.ToDto();
        }

        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task DepositBalance(long id, decimal amount, string currency)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            var finalAmount = Converter.Convert(amount, currency, account.Currency);
            account.Deposit(finalAmount);
            await _accountRepository.UpdateAsync(account);
        }

        public async Task<IEnumerable<AccountDto>> GetAllAsync()
        {
            var accounts = await _accountRepository.GetAllAsync();
            return accounts.Select(p => p.ToDto());
        }

        public async Task<AccountDto> GetByAccNumberAsync(string accNumber)
        {
            var account = await _accountRepository.GetByAccNumberAsync(accNumber);
            return account.ToDto();
        }

        public async Task<AccountDto> GetByIdAsync(long id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            return account.ToDto();
        }

        public async Task UpdateAsync(long id, UpdateAccountDto dto)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            account.UpdateStatus(dto.IsActive);

            await _accountRepository.UpdateAsync(account);
        }

        public async Task WithdrawBalance(long id, decimal amount, string currency)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            var finalAmount = Converter.Convert(amount, currency, account.Currency);
            account.Withdraw(finalAmount);
            await _accountRepository.UpdateAsync(account);
        }

        public async Task<AccountDto> GetBonusAccount()
        {
            var accounts = GetAllAsync();
            var account = (await accounts).FirstOrDefault(a => a.AccountType == AccountType.Bonus);
            return account;
        }

        public async Task<string> GenereateIban()
        {
            var lastIban = await _accountRepository.GetLastIban();
            int nextNumber = 1;
            if (string.IsNullOrEmpty(lastIban))
            {
                var numberPart = lastIban.Substring(2, 10);
                if (int.TryParse(numberPart, out int lastNumber))
                    nextNumber = lastNumber + 1;
            }
            var formatted = nextNumber.ToString("D10"); // 0001, 0002, ...
            return $"KZ5000{formatted}";
        }

    }
}
