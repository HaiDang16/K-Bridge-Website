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
                .Include(p => p.Replies)
                .Include(p => p.Topic) // Bao gồm thông tin về Topic của mỗi bài viết
            .ThenInclude(t => t.Forum) // Bao gồm thông tin về Forum của từng Topic
                .OrderByDescending(p => p.CreatedAt)
                .Take(4)
                .ToList();
        }
        public Post GetPostByID(int id)
        {
            return _context.Posts
                .Include(p => p.User) // Include user details of the post
                .Include(p => p.Topic) // Include topic details of the post
                    .ThenInclude(t => t.Forum) // Include forum details of the topic
                .Include(p => p.Replies) // Include replies of the post
                    .ThenInclude(r => r.User) // Include user details of each reply
                .FirstOrDefault(p => p.ID == id);
        }

        public void IncrementViewCount(int postId)
        {
            var post = _context.Posts.Find(postId);
            if (post != null)
            {
                post.ViewCount++;
                _context.SaveChanges();
            }
        }

        public List<Post> GetPostsByTopic(int topicId)
        {
            return _context.Posts
                .Where(p => p.TopicID == topicId)
                .Include(p => p.User)
                .Include(p => p.Replies)
                .Include(p => p.Topic)
                .ThenInclude(t => t.Forum)
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
        }

        public void UpdatePost(Post post)
        {
            post.UpdatedAt = DateTime.Now;
            _context.Posts.Update(post);
            _context.SaveChanges();
        }
        public Post GetPostWithVoteById(int id)
        {
            return _context.Posts
                .Include(p => p.Vote)
                .FirstOrDefault(p => p.ID == id);
        }
    }
}
