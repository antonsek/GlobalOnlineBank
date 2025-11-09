
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
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());

            modelBuilder.ApplyConfiguration(new ContragentConfiguration());
          
            modelBuilder.ApplyConfiguration(new TariffConfiguration());

            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        }
    }
}