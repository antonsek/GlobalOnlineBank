using GlobalOnlinebank.Domain.Entities;

namespace GlobalOnlinebank.Domain.Interfaces
{
    public interface IContragentRepository
    {
        Task<Contragent> GetByIdAsync(long id);
        Task<IEnumerable<Contragent>> GetAllAsync();
        Task<Contragent> AddAsync(Contragent contragent);
        Task UpdateAsync(Contragent contragent);
        Task DeleteAsync(long id);
    }
}
