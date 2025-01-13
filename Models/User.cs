namespace TicketSystem.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public bool isAdmin { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Ticket> Tickets { get; set; }
}
