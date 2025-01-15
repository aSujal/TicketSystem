using Microsoft.EntityFrameworkCore;
using TicketSystem.Data;
using TicketSystem.Models;

namespace TicketSystem.Services;

public class TicketService
{
    private readonly DataContext _context;
    public TicketService(DataContext context)
    {
        _context = context;
    }
    public async Task CreateAsync(Ticket ticket)
    {
        var response = await _context.Tickets.AddAsync(ticket);
        if(response.State == EntityState.Added)
        {
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new HttpRequestException("Ticket konnte nicht erstellt werden.");
        }
    }
    public async Task UpdateAsync(Ticket ticket)
    {
        var response = _context.Tickets.Update(ticket);
        if(response.State == EntityState.Modified)
        {
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new HttpRequestException("Ticket konnte nicht aktualisiert werden.");
        }
    }
    public async Task<List<Ticket?>> GetByUserIdAsync(int userId)
    {
        return _context.Tickets.Where(t => t.UserId == userId).Include(t => t.Sprint).ToList();
    }

    public async Task<List<Ticket?>> GetTicketsWithoutSprintAsync(int userId)
    {
        return _context.Tickets.Where(t => t.UserId == userId && t.SprintId == null).Include(t => t.Sprint).ToList();
    }

    public async Task<Ticket?> GetByIdAsync(int ticketId)
    {
        return await _context.Tickets.Include(t => t.Sprint).FirstOrDefaultAsync(t => t.Id == ticketId);
    }

    public async Task DeleteAsync(Ticket ticket)
    {
        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();
    }
}
