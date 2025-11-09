using GlobalOnlinebank.Application.DTOs;
using GlobalOnlinebank.Domain.Entities;

namespace GlobalOnlinebank.Application.Extensions;

public static class ContragentMappingExtensions
{
    /// <summary>
    /// Преобразует доменную сущность Contragent в DTO.
    /// </summary>
    public static ContragentDto ToDto(this Contragent entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        return new ContragentDto(
            Id: entity.Id,
            RuName: entity.RuName,
            KzName: entity.KzName,
            EnName: entity.EnName,
            Bin: entity.Bin,
            IsNew: entity.IsNew,
            IsActive: entity.IsActive,
            tariffId: entity.tariffId ?? 0
        );
    }

    /// <summary>
    /// Преобразует коллекцию Contragent в список DTO.
    /// </summary>
    public static IEnumerable<ContragentDto> ToDtoList(IEnumerable<Contragent> entities)
    {
        return entities?.Select(e => e.ToDto()) ?? Enumerable.Empty<ContragentDto>();
    }

    /// <summary>
    /// Преобразует DTO обратно в доменную сущность Contragent.
    /// </summary>
    public static Contragent ToEntity(this ContragentDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        return new Contragent(
            ruName: dto.RuName,
            kzName: dto.KzName,
            enName: dto.EnName,
            bin: dto.Bin
        );
    }
}
