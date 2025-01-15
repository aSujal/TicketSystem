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
    public async GetByUserIdAsync(int userId)
    {

    }
}
