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
                forum.Topics = topics.Where(t => t.ForumID == forum.ID && t.Status == "Active").ToList();
            return forums;
        }

        public List<Forum> GetForumsWithTopicsAndLatestPosts()
        {
            var forums = _context.Forums
                  .Include(f => f.Topics.Where(t => t.Status == "Active"))
                .ThenInclude(t => t.Posts.Where(p => p.Status == "Approved")
                                         .OrderByDescending(p => p.CreatedAt))
                .ThenInclude(p => p.User)
                .ToList();

            // Lọc chỉ lấy những chủ đề có bài viết
            foreach (var forum in forums)
            {
                forum.Topics = forum.Topics.Where(t => t.Posts.Take(1).Any()).ToList();
            }

            return forums;
        }
        public Forum GetTopicWithForumById(int id)
        {
            // Retrieve the forum including its topics and posts
            var forum = _context.Forums
                .Include(f => f.Topics)
                    .ThenInclude(t => t.Posts.OrderByDescending(p => p.CreatedAt))
                .ThenInclude(p => p.User)
                .FirstOrDefault(f => f.ID == id);

            // Check if the forum is null
            if (forum == null)
                return null;

            // Filter topics to only include those with posts
            forum.Topics = forum.Topics
                .Where(t => t.Posts.Any())
                .ToList();

            return forum;
        }

    }
}