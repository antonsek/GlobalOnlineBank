using GlobalOnlinebank.Application.DTOs;

namespace GlobalOnlinebank.Application.Interfaces;

public interface ITariffService
{
    /// <summary>
    /// Возвращает тариф, соответствующий количеству баллов.
    /// </summary>
    Task<TariffDto?> GetTariffByPointsAsync(int points, CancellationToken cancellationToken = default);

    /// <summary>
    /// Рассчитывает количество баллов за перевод по заданным параметрам.
    /// </summary>
    decimal CalculatePoints(decimal amountInKzt, string country, string segment);

    /// <summary>
    /// Возвращает описание выгоды клиента на основе его баллов.
    /// </summary>
    Task<string> GetClientBenefitDescriptionAsync(int points, CancellationToken cancellationToken = default);
    
    Task<List<TariffDto>> GetAllTariffsAsync(CancellationToken cancellationToken = default);
    Task<TariffDto?> GetTariffByIdAsync(long id, CancellationToken cancellationToken = default);
}