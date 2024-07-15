using K_Bridge.Models;
using Microsoft.EntityFrameworkCore;

namespace K_Bridge.Repositories
{
    public class EFPostRepository : IPostRepository
    {
        private KBridgeDbContext _context;
        public EFPostRepository(KBridgeDbContext context)
        {
            _context = context;
        }
        public IQueryable<Post> Posts => _context.Posts;
        public void SavePost(Post post)
        {
            // Kiểm tra xem có id này chưa
            if (post.ID == 0)
                _context.Posts.Add(post);
            _context.SaveChanges();
        }
        public List<Post> GetLatestPost()
        {
            return _context.Posts
                .Include(p => p.User)
                .OrderByDescending(p => p.CreatedAt)
                .Take(4)
                .ToList();
        }
    }
}
