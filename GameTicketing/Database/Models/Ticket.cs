namespace GameTicketing.Database.Models;

public class Ticket
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Department { get; set; }
    public string Description { get; set; }
    public TicketStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}