using GlobalOnlinebank.Domain.Entities;

namespace GlobalOnlinebank.Domain.Interfaces;

public interface ITariffRepository
{
    Task<List<Tariff>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Tariff?> GetByPointsAsync(int points, CancellationToken cancellationToken = default);
}