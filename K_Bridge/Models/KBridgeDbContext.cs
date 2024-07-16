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
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Reply> Replies => Set<Reply>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Topic>()
           .HasOne(t => t.Forum)
           .WithMany(f => f.Topics)
           .HasForeignKey(t => t.ForumID);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Topic)
                .WithMany(t => t.Posts)
                .HasForeignKey(p => p.TopicID);

            modelBuilder.Entity<Reply>()
                .HasOne(r => r.Post)
                .WithMany(p => p.Replies)
                .HasForeignKey(r => r.PostID);

            // Configure other relationships if necessary

            base.OnModelCreating(modelBuilder);
        }
    }
}