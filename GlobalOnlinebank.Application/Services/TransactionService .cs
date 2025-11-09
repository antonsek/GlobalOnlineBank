using GlobalOnlinebank.Application.DTOs;
using GlobalOnlinebank.Application.Extensions;
using GlobalOnlinebank.Application.Interfaces;
using GlobalOnlinebank.Domain.Entities;
using GlobalOnlinebank.Domain.Interfaces;
using GlobalOnlinebank.WebApi.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GlobalOnlinebank.Application.Services
{
    public class TransactionService : ITransactionService
    {
        IAccountService _accountService;
        ITariffService _tariffService;
        IContragentService _contragentService;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(IAccountService accountService, ITariffService tariffService, IContragentService contragentService,
                    ITransactionRepository transactionRepository)
        {
            _accountService = accountService;
            _tariffService = tariffService;
            _contragentService = contragentService;
            _transactionRepository = transactionRepository;
        }

        public async Task<TransferResponseDto> ExecuteAsync(TransferRequestDto request, CancellationToken ct)
        {
            // 1. Загрузка данных клиента
            try
            {
                long receiverAccountId = -1000;
                var senderAccount = await _accountService.GetByAccNumberAsync(request.SenderAccountNumber);
                var senderAccountComissionPayer = await _accountService.GetByAccNumberAsync(request.CommissionPayerAccountNumber);
                var senderData = await _contragentService.GetByIdAsync(senderAccount.ContragentID);
                var tariff = await _tariffService.GetTariffByIdAsync(senderData.tariffId);

                if (senderAccount == null || !senderAccount.IsActive)
                    throw new InvalidOperationException("Accounts are not valid");

                if (senderAccount.Balance < request.Amount)
                    throw new InvalidOperationException("Insufficient funds");


                // 1. Рассчитать комиссию
                decimal baseCommission = 7000m; // базовая комиссия 7000 KZT
                decimal bonusAmount = 0m;
                if (!string.IsNullOrEmpty(request.BonusAccountNumber))
                {
                    var senderBonusAccount = await _accountService.GetByAccNumberAsync(request.BonusAccountNumber);

                    // максимум 50% комиссии можно покрыть бонусами
                    var maxBonusCover = baseCommission * 0.5m;

                    if (senderBonusAccount.Balance >= maxBonusCover)
                    {
                        // хватает на полное покрытие половины комиссии
                        bonusAmount = maxBonusCover;
                        await _accountService.WithdrawBalance(senderBonusAccount.Id, bonusAmount, request.Currency);
                    }
                    else if (senderBonusAccount.Balance > 0)
                    {
                        // не хватает, списываем сколько есть
                        bonusAmount = senderBonusAccount.Balance;
                        await _accountService.WithdrawBalance(senderBonusAccount.Id, bonusAmount, request.Currency);
                    }
                }


                decimal commission = baseCommission - (baseCommission * tariff.CommissionDiscountPercent / 100) - bonusAmount;

                await _accountService.WithdrawBalance(senderAccountComissionPayer.Id, commission, request.Currency);

                // 2. Снять деньги у отправителя и начисление бонуса

                await _accountService.WithdrawBalance(senderAccount.Id, request.Amount, request.Currency);


                var bonusAccount = _accountService.GetBonusAccount();
                var newBonusPoints = _tariffService.CalculatePoints(Converter.Convert(request.Amount, request.Currency, "KZT"), request.RecipientCountry, request.PaymentPurpose);// 1 бонус за каждые 10 000 KZT перевода
                if (bonusAccount != null)
                {
                    var newBonusAccount = await _accountService.CreateAsync(new CreateAccountDto(senderData.Id, _accountService.GenereateIban().ToString(), "KZT", 0, AccountType.Bonus));
                    await _accountService.DepositBalance(newBonusAccount.Id, newBonusPoints, "KZT");
                }
                else
                {
                    await _accountService.DepositBalance(bonusAccount.Id, newBonusPoints, "KZT");
                }

                // 3. Пополнить получателя
                if (request.RecipientBankSwift == "HSBKKZKX")
                {
                    var receiverAccount = await _accountService.GetByAccNumberAsync(request.ReceiverAccountNumber);
                    var receiverData = await _contragentService.GetByIdAsync(receiverAccount.ContragentID);

                    receiverAccountId = receiverAccount.Id;

                    if (receiverAccount == null || !receiverAccount.IsActive)
                        throw new InvalidOperationException("Accounts are not valid");

                    await _accountService.DepositBalance(receiverAccount.Id, request.Amount, request.Currency);
                }

                // 4. Записать транзакцию
                var transaction = new Transaction(
                    senderAccount.Id,
                    receiverAccountId,
                    senderAccount.AccountNumber,
                    request.ReceiverAccountNumber,
                    request.Amount,
                    request.Currency,
                    request.RecipientName,
                    request.RecipientBankSwift,
                    request.RecipientBankName,
                    request.RecipientCountry,
                    request.RecipientCity,
                    request.RecipientAddress,
                    request.PaymentPurpose,
                    request.BonusAccountNumber,
                    request.CommissionPayerAccountNumber
                    );
                var response = await _transactionRepository.CreateAsync(transaction, ct);

                return response.ToDto();

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Transfer failed: " + ex.Message);
            }
        }

        public Task<Transaction> GetByIdAsync(long id, CancellationToken ct = default)
        {
            return _transactionRepository.GetByIdAsync(id, ct);
        }

        public Task<List<Transaction>> GetFromCurrentMonthAsync(CancellationToken ct = default)
        {
            var now = DateTime.UtcNow;
            var from = new DateTime(now.Year, now.Month, 1);
            var to = now;
            return _transactionRepository.GetByPeriodAsync(from, to, ct);
        }

        public Task<List<Transaction>> GetByPeriodAsync(DateTime from, DateTime to, CancellationToken ct = default)
        {
            return _transactionRepository.GetByPeriodAsync(from, to, ct);
        }
    }
}
