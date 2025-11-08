using GlobalOnlinebank.Application.DTOs;
using GlobalOnlinebank.Application.Extensions;
using GlobalOnlinebank.Application.Interfaces;
using GlobalOnlinebank.Domain.Interfaces;

namespace GlobalOnlinebank.Application.Services;

public class TariffService : ITariffService
    {
        private readonly ITariffRepository _tariffRepository;

        public TariffService(ITariffRepository tariffRepository)
        {
            _tariffRepository = tariffRepository;
        }
        
        public async Task<List<TariffDto>> GetAllTariffsAsync(CancellationToken cancellationToken = default)
        {
            var tariffs = await _tariffRepository.GetAllAsync(cancellationToken);
            return tariffs.Select(t => t.ToDto()).ToList();
        }

        public async Task<TariffDto?> GetTariffByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var tariffs = await _tariffRepository.GetAllAsync(cancellationToken);
            var tariff = tariffs.FirstOrDefault(t => t.Id == id);
            return tariff?.ToDto();
        }

        /// <summary>
        /// Определяет тариф по количеству баллов клиента.
        /// </summary>
        public async Task<TariffDto?> GetTariffByPointsAsync(int points, CancellationToken cancellationToken = default)
        {
            var tariff = await _tariffRepository.GetByPointsAsync(points, cancellationToken);
            return tariff?.ToDto();
        }

        /// <summary>
        /// Рассчитывает количество баллов по правилам ВЭД.
        /// Формула: (Объем перевода в KZT / 1 000 000) × К_страны × К_сегмента
        /// </summary>
        public decimal CalculatePoints(decimal amountInKzt, string country, string segment)
        {
            var kCountry = GetCountryCoefficient(country);
            var kSegment = GetSegmentCoefficient(segment);

            var points = (amountInKzt / 1_000_000m) * kCountry * kSegment;
            return Math.Round(points, 2);
        }

        /// <summary>
        /// Возвращает описание выгоды клиента (скидка, бонусы и т.п.)
        /// </summary>
        public async Task<string> GetClientBenefitDescriptionAsync(int points, CancellationToken cancellationToken = default)
        {
            var tariff = await _tariffRepository.GetByPointsAsync(points, cancellationToken);
            if (tariff == null)
                return "Нет активного тарифа";

            var description = $"{tariff.Name}: {tariff.Description}";
            if (tariff.CommissionDiscountPercent > 0)
                description += $" | Скидка {tariff.CommissionDiscountPercent}%";

            if (tariff.RateImprovementPercent > 0)
                description += $" | Улучшение курса +{tariff.RateImprovementPercent}%";

            if (tariff.HasPriorityService)
                description += " | Приоритетное обслуживание";

            if (tariff.HasPersonalManager)
                description += " | Персональный менеджер";

            return description;
        }

        #region Private helpers

        private static decimal GetCountryCoefficient(string country)
        {
            var normalized = country.ToLower();

            if (new[] { "россия", "узбекистан", "беларусь" }.Contains(normalized))
                return 1.2m;
            if (new[] { "китай", "индия", "корея" }.Contains(normalized))
                return 1.2m;
            if (new[] { "германия", "италия", "франция" }.Contains(normalized))
                return 1.3m;
            if (new[] { "оаэ", "саудовская аравия" }.Contains(normalized))
                return 1.3m;

            return 1.0m; // default
        }

        private static decimal GetSegmentCoefficient(string segment)
        {
            var normalized = segment.ToLower();

            return normalized switch
            {
                "импорт" => 1.3m,
                "экспорт" => 1.2m,
                "услуги" => 1.1m,
                _ => 1.0m
            };
        }

        #endregion
    }