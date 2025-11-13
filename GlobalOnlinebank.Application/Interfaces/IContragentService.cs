using GlobalOnlinebank.Application.DTOs;

namespace GlobalOnlinebank.Application.Interfaces
{
    public interface IContragentService
    {
        Task<ContragentDto> GetByIdAsync(long id);
        Task<IEnumerable<ContragentDto>> GetAllAsync();
        Task<ContragentDto> CreateAsync(CreateContragentDto dto);
        Task UpdateAsync(long id, UpdateContragentDto dto);
        Task AddLoyaltyPointsAsync(long contragentId, decimal points);
        Task DeleteAsync(long id);
    }
}
