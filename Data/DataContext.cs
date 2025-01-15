using Microsoft.EntityFrameworkCore;
using TicketSystem.Models;

namespace TicketSystem.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Sprint> Sprints { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Username).IsUnique();
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.HasOne(t => t.Sprint)
                .WithMany(s => s.Tickets)
                .HasForeignKey(t => t.SprintId)
                .OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(t => t.User)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Sprint>(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.HasOne(s => s.User)
                .WithMany(u => u.Sprints)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasMany(s => s.Tickets)
                .WithOne(t => t.Sprint)
                .HasForeignKey(t => t.SprintId);
        });
    }
}
