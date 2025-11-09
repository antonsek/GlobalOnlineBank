namespace GlobalOnlinebank.WebApi.Models
{
    public record TransferRequestDto
    (
        string SenderAccountNumber,
        string ReceiverAccountNumber,
        decimal Amount,
        string Currency,

        // Реквизиты получателя
        string RecipientName,
        string RecipientBankSwift,
        string RecipientBankName,
        string RecipientCountry,
        string RecipientCity,
        string RecipientAddress,
        string PaymentPurpose,

        // Доп. параметры
        string CommissionPayerAccountNumber,
        string? BonusAccountNumber,
        string? IdempotencyKey = null
    );
}
