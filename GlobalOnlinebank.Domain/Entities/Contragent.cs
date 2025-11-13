
using Shared;

namespace GlobalOnlinebank.Domain.Entities
{
    public class Contragent : BaseEntity
    {
        public string RuName { get; private set; }
        public string KzName { get; private set; }
        public string EnName { get; private set; }
        public string Bin { get; private set; }
        public bool IsNew { get; private set; }
        public bool IsActive { get; private set; }

        public List<Contragent> Partners { get; private set; } = new();
        public List<Contragent> PartnerOf { get; private set; } = new();

        public List<Account> Accounts { get; private set; }

        public Tariff? Tariff { get; private set; }
        
        public long TariffId { get; private set; }

        public decimal LoyaltyPoints { get; private set; }

        public Contragent(string ruName, string kzName,string enName, string bin)
        {
            if (string.IsNullOrWhiteSpace(ruName) && string.IsNullOrWhiteSpace(kzName) && string.IsNullOrWhiteSpace(enName))
                throw new ArgumentException("Name cannot be empty", nameof(ruName));
            if (bin.Length == 0)
                throw new ArgumentException("Bin cannot be empty", nameof(bin));

            RuName = ruName;
            KzName = kzName ?? "";
            EnName = enName ?? "";
            Bin = bin;
            IsActive = true;
            IsNew = true;
            Accounts = new List<Account>();
            LoyaltyPoints = 0m;
        }

        public void UpdateDetails(string ruName, string kzName,string enName, string bin, bool isActive, bool isNew)
        {
            if (string.IsNullOrWhiteSpace(ruName) && string.IsNullOrWhiteSpace(kzName) && string.IsNullOrWhiteSpace(enName))
                throw new ArgumentException("Name cannot be empty", nameof(ruName));
            if (bin.Length == 0)
                throw new ArgumentException("Bin cannot be empty", nameof(bin));

            RuName = ruName;
            KzName = kzName ?? "";
            EnName = enName ?? "";
            Bin = bin;
            IsActive = isActive;
            IsNew = isNew;
        }

        public void AddAccount(Account account)
        {
            if (account.AccountType == AccountType.Main && Accounts.Any(a => a.AccountType == AccountType.Main))
                throw new InvalidOperationException("Counterparty already has a main account.");

            Accounts.Add(account);
        }
        
        public void AddPartner(Contragent partner)
        {
            if (partner == null)
                throw new ArgumentNullException(nameof(partner));
            if (partner == this)
                throw new InvalidOperationException("Contragent cannot be a partner with itself.");
            if (Partners.Contains(partner))
                return;

            Partners.Add(partner);
            partner.PartnerOf.Add(this);
        }

        public void UpdateTariffAndApplyBonus(IEnumerable<Tariff> tariffs)
        {
            if (tariffs == null || !tariffs.Any())
                throw new ArgumentException("Список тарифов пуст");

            // Находим тариф, соответствующий текущему количеству бонусов
            var newTariff = tariffs
                .FirstOrDefault(t => LoyaltyPoints >= t.MinPoints && LoyaltyPoints <= t.MaxPoints);

            if (newTariff == null)
                return; // нет подходящего тарифа

            // Если тариф изменился
            if (Tariff.Id != newTariff.Id)
            {
                Tariff = newTariff;
                TariffId = newTariff.Id;

                // Находим бонусный счёт
                var bonusAccount = Accounts.FirstOrDefault(a => a.AccountType == AccountType.Bonus);

                if (bonusAccount == null)
                    throw new InvalidOperationException("У контрагента нет бонусного счёта.");

                // Зачисляем бонусные очки на бонусный счёт
                if (newTariff.BonusPoints > 0)
                {
                    bonusAccount.Deposit(newTariff.BonusPoints);
                }
            }
        }

        public void AddLoyalityBonus(decimal points)
        {
            if (points <= 0)
                throw new InvalidOperationException("Points must be positive.");

            LoyaltyPoints += points;
        }

    }
}
