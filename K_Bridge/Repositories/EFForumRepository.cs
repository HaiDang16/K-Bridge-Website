using K_Bridge.Models;
using Microsoft.EntityFrameworkCore;

namespace K_Bridge.Repositories
{
    public class EFForumRepository : IForumRepository
    {
        private KBridgeDbContext _context;
        public EFForumRepository(KBridgeDbContext context)
        {
            _context = context;
        }
        public IQueryable<Forum> Forums => _context.Forums;

        public List<Forum> GetForumWithTopics()
        {
            var forums = _context.Forums.ToList();
            var topics = _context.Topics.ToList();
            foreach (var forum in forums)
                forum.Topics = topics.Where(t => t.ForumID == forum.ID).ToList();
            return forums;
        }

        public List<Forum> GetForumsWithTopicsAndLatestPosts()
        {
            var forums = _context.Forums
                .Include(f => f.Topics)
                .ThenInclude(t => t.Posts.OrderByDescending(p => p.CreatedAt))
            .ThenInclude(p => p.User)
                .ToList();

            // Lọc chỉ lấy những chủ đề có bài viết
            foreach (var forum in forums)
            {
                forum.Topics = forum.Topics.Where(t => t.Posts.Take(1).Any()).ToList();
            }

            return forums;
        }


    }
}