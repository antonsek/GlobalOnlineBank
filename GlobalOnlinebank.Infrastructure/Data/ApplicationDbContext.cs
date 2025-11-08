
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