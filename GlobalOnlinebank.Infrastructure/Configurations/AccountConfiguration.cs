using GlobalOnlinebank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GlobalOnlinebank.Infrastructure.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .IsRequired();

            builder.Property(a => a.AccountNumber)
                .IsRequired()
                .HasMaxLength(34); // IBAN длина (примерно)

            builder.Property(a => a.Currency)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(a => a.Balance)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(a => a.IsActive)
                .IsRequired();
           
            builder.Property(a => a.AccountType)
                .IsRequired()
                .HasConversion<int>(); // хранится как int в БД

            // 🔗 Связь "Один контрагент → Несколько счетов"
            builder.HasOne(a => a.Contragent)
                   .WithMany(c => c.Accounts) // Коллекция счетов у контрагента
                   .HasForeignKey(a => a.ContragentID)
                   .OnDelete(DeleteBehavior.Restrict); // чтобы не удалялись счета при удалении контрагента

            // Индекс для быстрого поиска по номеру счёта
            builder.HasIndex(a => a.AccountNumber)
                   .IsUnique();
           
            builder.HasData(
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
