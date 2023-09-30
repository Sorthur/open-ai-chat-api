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
}