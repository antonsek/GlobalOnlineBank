
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

        public List<Contragent> Partners { get; private set; }

        public List<Account> Accounts { get; private set; }

        public Tariff? Tariff { get; private set; }
        
        public long? tariffId { get; private set; }

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
    }
}
