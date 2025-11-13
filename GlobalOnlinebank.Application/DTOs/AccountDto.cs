using GlobalOnlinebank.Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalOnlinebank.Application.DTOs
{
    public record AccountDto(long Id, long ContragentID, string AccountNumber, string Currency, decimal Balance, bool IsActive, AccountType AccountType);

    public record CreateAccountDto(long ContragentID, string AccountNumber, string Currency, decimal Balance, AccountType AccountType);

    public record UpdateAccountDto( decimal Balance, bool IsActive);
}
