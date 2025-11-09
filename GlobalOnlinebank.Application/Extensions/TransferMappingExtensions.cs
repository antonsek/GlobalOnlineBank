using GlobalOnlinebank.Application.DTOs;
using GlobalOnlinebank.Domain.Entities;
using GlobalOnlinebank.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalOnlinebank.Application.Extensions
{
    public static class TransferMappingExtensions
    {
        public static TransferResponseDto ToDto(this Transaction entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return new TransferResponseDto(
                TransactionId: entity.Id,
                SenderAccountNumber: entity.SenderAccountNumber,
                ReceiverAccountNumber: entity.ReceiverAccountNumber,
                Amount:  entity.Amount,
                Commission: entity.Commission,
                BonusPointsUsed: entity.BonusPointsUsed,
                BonusPointsEarned: entity.BonusPointsEarned,
                Currency: entity.Currency,
                CreatedAt: entity.CreatedAt,
                RecipientName: entity.RecipientName,
                RecipientBankSwift: entity.RecipientBankSwift,
                RecipientBankName: entity.RecipientBankName,
                RecipientCountry: entity.RecipientCountry,
                RecipientCity: entity.RecipientCity,
                RecipientAddress: entity.RecipientAddress,
                PaymentPurpose: entity.PaymentPurpose,
                BonusAccountNumber: entity.BonusAccountNumber,
                ComissionAccountNumber: entity.ComissionAccountNumber
            );
        }
    }
}
