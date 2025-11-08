using Shared;

namespace GlobalOnlinebank.Domain.Entities;

/// <summary>
/// Тарифная модель мотивации клиентов ВЭД.
/// </summary>
public class Tariff : BaseEntity
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    
    // Минимальный и максимальный порог баллов
    public int MinPoints { get; private set; }
    public int MaxPoints { get; private set; }

    // Процент скидки на комиссию
    public decimal CommissionDiscountPercent { get; private set; }

    // Повышение курса при обмене
    public decimal RateImprovementPercent { get; private set; }

    // Дополнительные бонусы
    public int BonusPoints { get; private set; }

    // Флаг приоритетного обслуживания
    public bool HasPriorityService { get; private set; }

    // Флаг наличия персонального менеджера
    public bool HasPersonalManager { get; private set; }

    public Tariff(
        string name,
        string description,
        int minPoints,
        int maxPoints,
        decimal commissionDiscountPercent,
        decimal rateImprovementPercent = 0,
        int bonusPoints = 0,
        bool hasPriorityService = false,
        bool hasPersonalManager = false)
    {
        Name = name;
        Description = description;
        MinPoints = minPoints;
        MaxPoints = maxPoints;
        CommissionDiscountPercent = commissionDiscountPercent;
        RateImprovementPercent = rateImprovementPercent;
        BonusPoints = bonusPoints;
        HasPriorityService = hasPriorityService;
        HasPersonalManager = hasPersonalManager;
    }

    public bool IsInRange(int points) =>
        points >= MinPoints && points <= MaxPoints;
}