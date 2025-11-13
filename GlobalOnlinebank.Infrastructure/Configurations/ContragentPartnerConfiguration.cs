using GlobalOnlinebank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalOnlinebank.Infrastructure.Configurations;

public class ContragentPartnerConfiguration: IEntityTypeConfiguration<ContragentPartner>
{
    public void Configure(EntityTypeBuilder<ContragentPartner> builder)
    {
        builder.ToTable("ContragentPartners");
        builder.HasKey(cp => new { cp.ContragentId, cp.PartnerId });

        builder.HasData(
            new ContragentPartner { ContragentId = -1, PartnerId = -2 },
            new ContragentPartner { ContragentId = -2, PartnerId = -1 }
        );
    }
}