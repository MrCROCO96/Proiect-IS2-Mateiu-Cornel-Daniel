namespace GameTicketing.DataTransferObjects;

public class TicketRecord
{
    public int Id { get; set; }
    public string TipTichet { get; set; } = null!;
    public string Severitate { get; set; } = null!;
    public string Stadiu { get; set; } = null!;
}