using K_Bridge.Models;
using Microsoft.EntityFrameworkCore;

namespace K_Bridge.Models
{
    public class KBridgeDbContext : DbContext
    {
        public KBridgeDbContext(DbContextOptions<KBridgeDbContext> options)
        : base(options) { }
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Stats> Statses => Set<Stats>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Topic> Topics => Set<Topic>();
        public DbSet<Admin_Accounts> Admin_Accounts => Set<Admin_Accounts>();
        public DbSet<Forum> Forums => Set<Forum>();
        public DbSet<GlobalChat> GlobalChats => Set<GlobalChat>();
    }
}
