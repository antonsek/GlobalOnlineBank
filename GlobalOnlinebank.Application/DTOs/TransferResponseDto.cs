namespace GlobalOnlinebank.WebApi.Models
{
    public record TransferResponseDto
    (
        long TransactionId,
        string SenderAccountNumber,
        string ReceiverAccountNumber,
        decimal Amount,
        decimal Commission,
        int BonusPointsUsed,
        int BonusPointsEarned,
        string Currency,
        DateTime CreatedAt,

        // Реквизиты получателя
        string RecipientName,
        string RecipientBankSwift,
        string RecipientBankName,
        string RecipientCountry,
        string RecipientCity,
        string RecipientAddress,
        string PaymentPurpose,

        string BonusAccountNumber,
        string ComissionAccountNumber
    );
}
