namespace GlobalOnlinebank.Domain.Entities;

public class ContragentPartner
{
    public long ContragentId { get; set; }
    public Contragent Contragent { get; set; }

    public long PartnerId { get; set; }
    public Contragent Partner { get; set; }
}