
using GlobalOnlinebank.Domain.Entities;
using GlobalOnlinebank.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace GlobalOnlinebank.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contragent> Contragents { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());

            modelBuilder.Entity<Contragent>().HasKey(p => p.Id);

            // Seed data
            modelBuilder.Entity<Contragent>().HasData(
                new Contragent("ТОО Колбаса","ТОО Колбаса","LLP Kolbasa",  "961545123222") { Id = -1, CreatedDate = new DateTime(2024, 11, 8, 0, 0, 0, DateTimeKind.Utc)},
                new Contragent("ТОО Рахат","ТОО Рахат","LLP Rahat",  "961545123251") { Id = -2, CreatedDate = new DateTime(2024, 11, 8, 0, 0, 0, DateTimeKind.Utc) }
            );
            
            modelBuilder.Entity<Tariff>().HasData(
                new Tariff(
                    name: "Base",
                    description: "0–3000 баллов — стандартные условия",
                    minPoints: 0,
                    maxPoints: 2999,
                    commissionDiscountPercent: 0
                ) { Id = -1, CreatedDate = new DateTime(2024, 11, 8, 0, 0, 0, DateTimeKind.Utc) },

                new Tariff(
                    name: "Prime",
                    description: "3000–6000 баллов — скидка 3%, бонус 10 000 баллов",
                    minPoints: 3000,
                    maxPoints: 5999,
                    commissionDiscountPercent: 3,
                    bonusPoints: 10000
                ) { Id = -2, CreatedDate = new DateTime(2024, 11, 8, 0, 0, 0, DateTimeKind.Utc) },

                new Tariff(
                    name: "Prime PRO",
                    description: "6000–8000 баллов — скидка 5%, бонус 20 000 баллов, улучшение курса +0.05%",
                    minPoints: 6000,
                    maxPoints: 7999,
                    commissionDiscountPercent: 5,
                    rateImprovementPercent: 0.05m,
                    bonusPoints: 20000
                ) { Id = -3, CreatedDate = new DateTime(2024, 11, 8, 0, 0, 0, DateTimeKind.Utc) },

                new Tariff(
                    name: "Advance",
                    description: "8000–10000 баллов — скидка 7%, бонус 30 000, приоритетная обработка",
                    minPoints: 8000,
                    maxPoints: 9999,
                    commissionDiscountPercent: 7,
                    bonusPoints: 30000,
                    hasPriorityService: true
                ) { Id = -4, CreatedDate = new DateTime(2024, 11, 8, 0, 0, 0, DateTimeKind.Utc) },

                new Tariff(
                    name: "Executive",
                    description: "10 000–15 000 баллов — скидка 10%, бонус 40 000, персональный менеджер",
                    minPoints: 10000,
                    maxPoints: 14999,
                    commissionDiscountPercent: 10,
                    bonusPoints: 40000,
                    rateImprovementPercent: 0.05m,
                    hasPriorityService: true,
                    hasPersonalManager: true
                ) { Id = -5, CreatedDate = new DateTime(2024, 11, 8, 0, 0, 0, DateTimeKind.Utc) },

                new Tariff(
                    name: "Privilege",
                    description: "15 000+ баллов — скидка 15%, бонус 50 000, полный пакет привилегий",
                    minPoints: 15000,
                    maxPoints: int.MaxValue,
                    commissionDiscountPercent: 15,
                    bonusPoints: 50000,
                    rateImprovementPercent: 0.1m,
                    hasPriorityService: true,
                    hasPersonalManager: true
                ) { Id = -6, CreatedDate = new DateTime(2024, 11, 8, 0, 0, 0, DateTimeKind.Utc) }
            );

            modelBuilder.Entity<Account>().HasData(
                new Account(-1, "KZ1000000001", "KZT", AccountType.Main)
                {
                    Id = -1,
                    CreatedDate = new DateTime(2024, 11, 8, 0, 0, 0, DateTimeKind.Utc),
                    Balance = 10000m,
                    IsActive = true,
                },
                new Account(-1, "KZ1000000002", "KZT", AccountType.Bonus)
                {
                    Id = -2,
                    Balance = 500m,
                    CreatedDate = new DateTime(2024, 11, 8, 0, 0, 0, DateTimeKind.Utc),
                    IsActive = true,
                },
                new Account(-1, "KZ1000000003", "USD", AccountType.Main)
                {
                    Id = -3,
                    Balance = 20000m,
                    CreatedDate = new DateTime(2024, 11, 8, 0, 0, 0, DateTimeKind.Utc),
                    IsActive = true,
                }
            );

        }
    }
}