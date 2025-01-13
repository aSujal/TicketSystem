using Microsoft.EntityFrameworkCore;
using TicketSystem.Models;

namespace TicketSystem.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Sprint> Sprints { get; set; }
    public DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>().HasIndex(u => u.Username);

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.HasOne(t => t.User)
            .WithMany(u => u.Tickets)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(t => t.Sprint)
            .WithMany(s => s.Tickets)
            .HasForeignKey(t => t.SprintId)
            .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<Sprint>().HasKey(s => s.Id);
    }
}
