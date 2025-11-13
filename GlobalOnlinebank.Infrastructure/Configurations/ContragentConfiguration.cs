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
public class ContragentConfiguration : IEntityTypeConfiguration<Contragent>
{
    public void Configure(EntityTypeBuilder<Contragent> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder
            .HasMany(c => c.Partners)
            .WithMany(c => c.PartnerOf)
            .UsingEntity<ContragentPartner>(
                j => j.HasOne(cp => cp.Partner)
                    .WithMany()
                    .HasForeignKey(cp => cp.PartnerId),
                j => j.HasOne(cp => cp.Contragent)
                    .WithMany()
                    .HasForeignKey(cp => cp.ContragentId)
            );
            builder.HasOne(c => c.Tariff)
              .WithMany(t => t.Contragents) // если у Tariff нет коллекции компаний
              .HasForeignKey(c => c.TariffId);

            builder.Property(c => c.TariffId)
                .HasDefaultValue(-1);
        
            // Seed data
        builder.HasData(
            new Contragent("ТОО Колбаса","ТОО Колбаса","LLP Kolbasa",  "961545123222") { Id = -1, CreatedDate = new DateTime(2024, 11, 8, 0, 0, 0, DateTimeKind.Utc)},
            new Contragent("ТОО Рахат","ТОО Рахат","LLP Rahat",  "961545123251") { Id = -2, CreatedDate = new DateTime(2024, 11, 8, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
}
