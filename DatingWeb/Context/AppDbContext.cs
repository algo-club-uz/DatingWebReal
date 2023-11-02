using DatingWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingWeb.Context;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>()
            .HasMany(u => u.Messages)
            .WithOne(b => b.Chat)
            .HasForeignKey(b => b.ChatId);
    }

    public DbSet<User> Users { get; set; } 
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
}