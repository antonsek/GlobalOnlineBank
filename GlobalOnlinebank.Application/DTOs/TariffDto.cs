namespace GlobalOnlinebank.Application.DTOs;

public record TariffDto(
    long Id,
    string Name,
    string Description,
    int MinPoints,
    int MaxPoints,
    decimal CommissionDiscountPercent,
    decimal RateImprovementPercent,
    int BonusPoints,
    bool HasPriorityService,
    bool HasPersonalManager
);