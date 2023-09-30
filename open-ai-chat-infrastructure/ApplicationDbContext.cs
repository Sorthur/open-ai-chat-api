using Microsoft.EntityFrameworkCore;
using open_ai_chat_domain.Entities;

namespace open_ai_chat_infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<ChatConfiguration> ChatConfigurations { get; set; }
    public DbSet<Conversation> Conversations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasMany(p => p.ChatConfigurations)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<User>()
            .HasMany(p => p.Conversations)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}