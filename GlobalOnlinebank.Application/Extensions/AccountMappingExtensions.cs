using GlobalOnlinebank.Application.DTOs;
using GlobalOnlinebank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalOnlinebank.Application.Extensions
{
    public static class AccountMappingExtensions
    {
        public static AccountDto ToDto(this Account entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return new AccountDto(
                Id: entity.Id,
                ContragentID: entity.ContragentID,
                AccountNumber: entity.AccountNumber,
                Currency: entity.Currency,
                Balance: entity.Balance,
                IsActive: entity.IsActive,
                AccountType: entity.AccountType

            );
        }

        /// <summary>
        /// Преобразует коллекцию Contragent в список DTO.
        /// </summary>
        public static IEnumerable<AccountDto> ToDtoList(IEnumerable<Account> entities)
        {
            return entities?.Select(e => e.ToDto()) ?? Enumerable.Empty<AccountDto>();
        }

        /// <summary>
        /// Преобразует DTO обратно в доменную сущность Contragent.
        /// </summary>
        public static Account ToEntity(this AccountDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new Account(
                contragernId: dto.ContragentID,
                accountNumber: dto.AccountNumber, // временно, номер можно сгенерировать в сервисе
                currency: dto.Currency,
                accountType: dto.AccountType
            );
        }

    }
}
