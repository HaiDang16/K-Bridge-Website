using K_Bridge.Models;
using Microsoft.Extensions.Hosting;

namespace K_Bridge.Repositories
{
    public class EFLikeRepository : ILikeRepository
    {
        private KBridgeDbContext _context;
        public EFLikeRepository(KBridgeDbContext context)
        {
            _context = context;
        }
        public IQueryable<Post_Like> Post_Likes => _context.Post_Likes;

        public void SavePostLike(Post_Like like)
        {
            if (like.ID == 0)
                _context.Post_Likes.Add(like);
            _context.SaveChanges();
        }

        public Post_Like GetExistPostLike(int postId, int userId)
        {
            return _context.Post_Likes.FirstOrDefault(pl => pl.PostID == postId && pl.UserID == userId);
        }
        public void UpdateLike(Post_Like postLike, bool isLike)
        {
            postLike.IsLike = isLike;
            _context.SaveChanges();
        }
        public int GetLikeCount(int postId)
        {
            return _context.Post_Likes.Count(pl => pl.PostID == postId && pl.IsLike);
        }
        public int GetDislikeCount(int postId)
        {
            return _context.Post_Likes.Count(pl => pl.PostID == postId && !pl.IsLike);
        }

        public void DeleteLike(int postId, int userId)
        {
            var like = _context.Post_Likes.FirstOrDefault(pl => pl.PostID == postId && pl.UserID == userId);
            if (like != null)
            {
                _context.Post_Likes.Remove(like);
                _context.SaveChanges();
            }
        }
        public void DeleteExistPostLike(Post_Like postLike)
        {
            _context.Post_Likes.Remove(postLike);
            _context.SaveChanges();
        }
    }
}
