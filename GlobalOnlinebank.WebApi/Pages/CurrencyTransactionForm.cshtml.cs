using GlobalOnlinebank.Application.DTOs;
using GlobalOnlinebank.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class CurrencyTransferModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    public class TransferFormModel
    {
        public string SenderAccountNumber { get; set; } = "";
        public string ReceiverAccountNumber { get; set; } = "";
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "";
        public string RecipientName { get; set; } = "";
        public string RecipientBankSwift { get; set; } = "";
        public string RecipientBankName { get; set; } = "";
        public string RecipientCountry { get; set; } = "";
        public string RecipientCity { get; set; } = "";
        public string RecipientAddress { get; set; } = "";
        public string PaymentPurpose { get; set; } = "";
        public string CommissionPayerAccountNumber { get; set; } = "";
        public string? BonusAccountNumber { get; set; } = "";
        public string? IdempotencyKey { get; set; } = "";
    }

    public CurrencyTransferModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public TransferFormModel Form { get; set; } = new();

    public List<AccountDto> Accounts { get; set; } = new();

    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient("Api");
        Accounts = await client.GetFromJsonAsync<List<AccountDto>>(
            "api/account/by-contragent/-1");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var client = _httpClientFactory.CreateClient("Api");
        var response = await client.PostAsJsonAsync("api/transaction", Request);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", "Ошибка при создании транзакции");
            return Page();
        }

        var created = await response.Content.ReadFromJsonAsync<TransferResponseDto>();
        TempData["CreatedTransactionId"] = created?.TransactionId;

        return RedirectToPage("Success");
    }
}