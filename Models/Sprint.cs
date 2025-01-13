namespace TicketSystem.Models;

public class Sprint
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<Ticket> Tickets { get; set; }
    public string? Color { get; set; } = "#FFF1D0";
}
