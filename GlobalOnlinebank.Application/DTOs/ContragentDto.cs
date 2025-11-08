
namespace GlobalOnlinebank.Application.DTOs
{
    public record ContragentDto(long Id, string RuName, string KzName,string EnName, string Bin, bool IsNew, bool IsActive);

    public record CreateContragentDto(string RuName, string KzName,string EnName, string Bin);

    public record UpdateContragentDto(string RuName, string KzName,string EnName, string Bin, bool IsNew, bool IsActive);
}
