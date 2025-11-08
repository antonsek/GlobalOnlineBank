using GlobalOnlinebank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }
    }
}
