using GlobalOnlinebank.Domain.Entities;
using GlobalOnlinebank.Domain.Interfaces;
using GlobalOnlinebank.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GlobalOnlinebank.Infrastructure.Repositories;

public class TariffRepository : ITariffRepository
{
    private readonly ApplicationDbContext _context;

    public TariffRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Tariff>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _context.Tariffs.OrderBy(t => t.MinPoints).ToListAsync(cancellationToken);

    public async Task<Tariff?> GetByPointsAsync(int points, CancellationToken cancellationToken = default) =>
        await _context.Tariffs.FirstOrDefaultAsync(t => t.MinPoints <= points && t.MaxPoints >= points, cancellationToken);

}