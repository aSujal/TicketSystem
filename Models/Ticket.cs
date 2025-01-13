namespace TicketSystem.Models;

public class Ticket
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int Points { get; set; }
    public TicketStatus Status { get; set; } = TicketStatus.Offen;
    public string? Color { get; set; } = "#BDD5EA";
    public string? Emoji { get; set; } = "📝";
    public int SprintId {  get; set; }
    public Sprint Sprint { get; set; }
}

