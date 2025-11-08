using GlobalOnlinebank.Domain.Entities;
using GlobalOnlinebank.Domain.Interfaces;
using GlobalOnlinebank.Application.DTOs;
using GlobalOnlinebank.Application.Extensions;
using GlobalOnlinebank.Application.Interfaces;

namespace GlobalOnlinebank.Application.Services
{
    public class ContragentService : IContragentService
    {
        private readonly IContragentRepository _contragentRepository;

        public ContragentService(IContragentRepository contragentRepository)
        {
            _contragentRepository = contragentRepository;
        }

        public async Task<ContragentDto> GetByIdAsync(long id)
        {
            var contragent = await _contragentRepository.GetByIdAsync(id);
            return contragent.ToDto();
        }

        public async Task<IEnumerable<ContragentDto>> GetAllAsync()
        {
            var contragents = await _contragentRepository.GetAllAsync();
            return contragents.Select(p => p.ToDto());
        }

        public async Task<ContragentDto> CreateAsync(CreateContragentDto dto)
        {
            var contragent = new Contragent(dto.RuName, dto.KzName, dto.EnName, dto.Bin);
            var created = await _contragentRepository.AddAsync(contragent);
            return created.ToDto();;
        }

        public async Task UpdateAsync(long id, UpdateContragentDto dto)
        {
            var contragent = await _contragentRepository.GetByIdAsync(id);
            contragent.UpdateDetails(dto.RuName, dto.KzName, dto.EnName, dto.Bin, dto.IsActive, dto.IsNew);
            await _contragentRepository.UpdateAsync(contragent);
        }

        public async Task DeleteAsync(long id)
        {
            await _contragentRepository.DeleteAsync(id);
        }
    }
}
