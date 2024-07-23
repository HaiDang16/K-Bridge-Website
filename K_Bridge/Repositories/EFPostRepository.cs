using K_Bridge.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            .Where(p => p.Status == "Approved")
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
                     .Include(p => p.Vote)
            .ThenInclude(v => v.VoteOptions)
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
                .Where(p => p.Status == "Approved")
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
                 .ThenInclude(v => v.VoteOptions)
                .FirstOrDefault(p => p.ID == id);
        }
        public Reply GetReplyById(int id)
        {
            return _context.Replies
            .Include(r => r.User)
            .FirstOrDefault(r => r.ID == id);
        }
        public void RemoveReply(Reply reply)
        {
            _context.Replies.Remove(reply);
            _context.SaveChanges();
        }

        public IEnumerable<Post> GetPostsByTopicFilter(int topicId)
        {
            return _context.Posts
                .Where(p => p.TopicID == topicId)
                .Include(p => p.User)
                .Include(p => p.Replies)
                .Include(p => p.Topic)
                .ThenInclude(t => t.Forum)
                .Where(p => p.Status == "Approved")
                .OrderByDescending(p => p.CreatedAt)
                .AsQueryable()
                .ToList();
        }
        public IEnumerable<Post> PostsFilterTrending(IEnumerable<Post> posts)
        {
            var sevenDaysAgo = DateTime.Now.AddDays(-7);
            return posts
                .Select(p => new
                {
                    Post = p,
                    LikesInLast7Days = _context.Post_Likes.Count(l => l.PostID == p.ID && l.CreatedAt >= sevenDaysAgo)
                })
                    .OrderByDescending(p => p.LikesInLast7Days)
                    .Select(p => p.Post)
                     .AsQueryable()
                .ToList();
        }
        public IEnumerable<Post> PostsFilterHelpful(IEnumerable<Post> posts)
        {
            return posts
                .Select(p => new
                {
                    Post = p,
                    TotalLikes = _context.Post_Likes.Count(l => l.PostID == p.ID)
                })
                .OrderByDescending(p => p.TotalLikes)
                .Select(p => p.Post)
                .AsQueryable()
                .ToList();
        }

        public List<Post> GetAllPostsWithTopicPaging(int topicId, int pageIndex, int pageSize)
        {
            return _context.Posts
                .Where(t => t.TopicID == topicId)
                .OrderBy(p => p.Status == "Pending" ? 0 :
                              p.Status == "Approved" ? 1 :
                              p.Status == "Rejected" ? 2 :
                              p.Status == "Blocked" ? 3 : 4)
                .ThenByDescending(p => p.CreatedAt)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
        public int CountPostWithTopic(int topicId)
        {
            return _context.Posts
               .Where(t => t.TopicID == topicId)
               .Count();
        }
    }
}
