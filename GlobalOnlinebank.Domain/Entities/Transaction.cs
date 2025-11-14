using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalOnlinebank.Domain.Entities
{
    public class Transaction: BaseEntity
    {
        public long SenderAccountId { get; private set; }
        public long? ReceiverAccountId { get; private set; }
        public string SenderAccountNumber { get; private set; }
        public string ReceiverAccountNumber { get; private set; }
        public decimal Amount { get; private set; }
        public decimal Commission { get; private set; }
        public decimal BonusPointsUsed { get; private set; }
        public decimal BonusPointsEarned { get; private set; }
        public string Currency { get; private set; }
        public DateTime CreatedAt { get; private set; }

        // Навигация
        public Account SenderAccount { get; private set; }
        public Account ReceiverAccount { get; private set; }

        // Доп. поля из форм (для SWIFT/межбанк)
        public string RecipientName { get; private set; } = default!;
        public string RecipientBankSwift { get; private set; } = default!;
        public string RecipientBankName { get; private set; } = default!;
        public string RecipientCountry { get; private set; } = default!;
        public string RecipientCity { get; private set; } = default!;
        public string RecipientAddress { get; private set; } = default!;
        public string PaymentPurpose { get; private set; } = default!;

        public string? BonusAccountNumber { get; private set; } = default!;
        public string ComissionAccountNumber { get; private set; } = default!;

        private Transaction() { }

        public Transaction(
            long senderAccountId,
            long? receiverAccountId,
            string senderAccountNumber,
            string receiverAccountNumber,
            decimal amount,
            decimal commission,
            string currency,
            string recipientName,
            string recipientBankSwift,
            string recipientBankName,
            string recipientCountry,
            string recipientCity,
            string recipientAddress,
            string paymentPurpose,
            string bonusAccountNumber,
            string comissionAccountNumber,
            decimal bonusPointsUsed,
            decimal bonusPointsEarned)
        {
            SenderAccountId = senderAccountId;
            ReceiverAccountId = receiverAccountId;
            SenderAccountNumber = senderAccountNumber;
            ReceiverAccountNumber = receiverAccountNumber;
            Amount = amount;
            Currency = currency;
            Commission = commission;
            RecipientName = recipientName;
            RecipientBankSwift = recipientBankSwift;
            RecipientBankName = recipientBankName;
            RecipientCountry = recipientCountry;
            RecipientCity = recipientCity;
            RecipientAddress = recipientAddress;
            PaymentPurpose = paymentPurpose;
            CreatedAt = DateTime.UtcNow;
            BonusAccountNumber = bonusAccountNumber;
            ComissionAccountNumber = comissionAccountNumber;
            BonusPointsUsed = bonusPointsUsed;
            BonusPointsEarned = bonusPointsEarned;
        }
    }
}
