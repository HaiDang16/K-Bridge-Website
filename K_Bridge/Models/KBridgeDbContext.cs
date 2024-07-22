using K_Bridge.Models;
using Microsoft.EntityFrameworkCore;

namespace K_Bridge.Models
{
    public class KBridgeDbContext : DbContext
    {
        public KBridgeDbContext(DbContextOptions<KBridgeDbContext> options)
        : base(options) { }
        public DbSet<User> Users => Set<User>();
        public DbSet<Topic> Topics => Set<Topic>();
        public DbSet<Admin_Accounts> Admin_Accounts => Set<Admin_Accounts>();
        public DbSet<Forum> Forums => Set<Forum>();
        public DbSet<GlobalChat> GlobalChats => Set<GlobalChat>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Reply> Replies => Set<Reply>();
        public DbSet<Post_Like> Post_Likes => Set<Post_Like>();
        public DbSet<Reply_Like> Reply_Likes => Set<Reply_Like>();
        public DbSet<Vote> Votes => Set<Vote>();
        public DbSet<VoteOption> VoteOptions => Set<VoteOption>();

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
                 .HasForeignKey(r => r.PostID)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reply>()
               .HasOne(r => r.User)
               .WithMany(u => u.Replies)
               .HasForeignKey(r => r.UserID)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Post_Like>()
                .HasOne(r => r.Post)
                .WithMany(p => p.Post_Likes)
                .HasForeignKey(r => r.PostID);

            modelBuilder.Entity<Post_Like>()
                .HasOne(r => r.User)
                .WithMany(p => p.Post_Likes)
                .HasForeignKey(r => r.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reply_Like>()
                .HasOne(r => r.Reply)
                .WithMany(p => p.Reply_Likes)
                .HasForeignKey(r => r.ReplyID);

            modelBuilder.Entity<Reply_Like>()
                .HasOne(r => r.User)
                .WithMany(p => p.Reply_Likes)
                .HasForeignKey(r => r.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VoteOption>()
                .HasOne(r => r.Vote)
                .WithMany(p => p.VoteOptions)
                .HasForeignKey(r => r.VoteID);

            modelBuilder.Entity<UserVote>()
                .HasOne(uv => uv.User)
                .WithMany(u => u.UserVotes)
                .HasForeignKey(uv => uv.UserID);

            modelBuilder.Entity<UserVote>()
               .HasOne(uv => uv.VoteOption)
               .WithMany(vo => vo.UserVotes)
               .HasForeignKey(uv => uv.VoteOptionID)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Vote>()
               .HasOne(v => v.Post)
               .WithOne(p => p.Vote)
               .HasForeignKey<Vote>(v => v.PostID);

            base.OnModelCreating(modelBuilder);
        }
    }
}