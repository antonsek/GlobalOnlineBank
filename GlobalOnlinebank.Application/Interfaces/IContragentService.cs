using GlobalOnlinebank.Application.DTOs;

namespace GlobalOnlinebank.Application.Interfaces
{
    public interface IContragentService
    {
        Task<ContragentDto> GetByIdAsync(long id);
        Task<IEnumerable<ContragentDto>> GetAllAsync();
        Task<ContragentDto> CreateAsync(CreateContragentDto dto);
        Task UpdateAsync(long id, UpdateContragentDto dto);
        Task DeleteAsync(long id);
    }
}
