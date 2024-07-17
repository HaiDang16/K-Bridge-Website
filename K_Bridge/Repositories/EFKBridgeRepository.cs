using K_Bridge.Models;
using Microsoft.EntityFrameworkCore.Migrations;

namespace K_Bridge.Repositories
{
    public class EFKBridgeRepository : IKBridgeRepository
    {
        private KBridgeDbContext _context;
        public EFKBridgeRepository(KBridgeDbContext ctx)
        {
            _context = ctx;
        }
        public IQueryable<Category> Categories => _context.Categories;
        public IQueryable<Stats> Statses => _context.Statses;
        public IQueryable<User> Users => _context.Users;
        public IQueryable<Topic> Topics => _context.Topics;
        public IQueryable<Admin_Accounts> Admin_Accounts => _context.Admin_Accounts;
        public IQueryable<GlobalChat> GlobalChats => _context.GlobalChats;
        public IQueryable<Post> Posts => _context.Posts;

        public int GetTotalUsers() => _context.Users.Count();
        public int GetTotalTopics() => _context.Topics.Count();
        public int GetTotalPosts() => _context.Posts.Count();

        public int GetRecentUsers() => _context.Users.Count(u => u.CreatedAt >= DateTime.Now.AddDays(-7));
        public int GetRecentTopics() => _context.Topics.Count(t => t.CreatedAt >= DateTime.Now.AddDays(-7));
        public int GetRecentPosts() => _context.Posts.Count(p => p.CreatedAt >= DateTime.Now.AddDays(-7));
    }

}
