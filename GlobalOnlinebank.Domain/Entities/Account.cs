using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalOnlinebank.Domain.Entities
{
    public class Account: BaseEntity
    {
        public long ContragentID { get; private set; } // связь с контрагентом
        public string AccountNumber { get; private set; } // IBAN или уникальный номер счёта
        public string Currency { get; private set; } // например, "KZT", "USD"
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }

        public AccountType AccountType { get; set; }

        // Навигационное свойство
        public Contragent Contragent { get; private set; }

        private Account() { } // для EF

        public Account(long contragernId, string accountNumber, string currency, AccountType accountType)
        {
            ContragentID = contragernId;
            AccountNumber = accountNumber;
            Currency = currency;
            Balance = 0;
            IsActive = true;
            AccountType = accountType;

        }

        public void Deposit(decimal amount)
        {
            if (amount < 0)
                throw new InvalidOperationException("Deposit amount must be positive.");

            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount < 0)
                throw new InvalidOperationException("Withdraw amount must be positive.");
            if (Balance < amount)
                throw new InvalidOperationException("Insufficient funds.");

            Balance -= amount;
        }

        public void UpdateStatus(bool isActive)
        {
            IsActive = isActive;
        }
    }
    public enum AccountType
    {
        Main = 1,
        Bonus = 2
    }
}
