using GlobalOnlinebank.Application.DTOs;
using GlobalOnlinebank.Domain.Entities;

namespace GlobalOnlinebank.Application.Extensions;

public static class TariffMappingExtensions
{
    public static TariffDto ToDto(this Tariff entity) =>
        new TariffDto(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.MinPoints,
            entity.MaxPoints,
            entity.CommissionDiscountPercent,
            entity.RateImprovementPercent,
            entity.BonusPoints,
            entity.HasPriorityService,
            entity.HasPersonalManager
        );

    public static IEnumerable<TariffDto> ToDtoList(this IEnumerable<Tariff> entities) =>
        entities.Select(e => e.ToDto());
}