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
        private readonly ITariffRepository _tariffRepository;
        public ContragentService(IContragentRepository contragentRepository, ITariffRepository tariffRepository)
        {
            _contragentRepository = contragentRepository;
            _tariffRepository = tariffRepository;
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

        public async Task AddLoyaltyPointsAsync(long contragentId, decimal points)
        {
            if (points <= 0)
                throw new ArgumentException("Points must be positive.", nameof(points));

            // Загружаем контрагента с его счетами
            var contragent = await _contragentRepository.GetByIdAsync(contragentId);
            if (contragent == null)
                throw new InvalidOperationException("Contragent not found.");

            // Увеличиваем накопленные бонусы
            contragent.AddLoyalityBonus(points);

            var tariffs = await _tariffRepository.GetAllAsync();

            contragent.UpdateTariffAndApplyBonus(tariffs);

            await _contragentRepository.UpdateAsync(contragent);
        }
    }
}
