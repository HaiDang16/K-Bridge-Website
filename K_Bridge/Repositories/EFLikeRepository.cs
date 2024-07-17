using K_Bridge.Models;

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

        public void SavePostLike(Post_Like like) { }

        public Post_Like GetPostLike(int postId, int userId)
        {
            return _context.Post_Likes.FirstOrDefault(pl => pl.PostID == postId && pl.UserID == userId);
        }
        public void UpdateLike(int postId, int userId, bool isLike)
        {
            var ỉtem = _context.Post_Likes.FirstOrDefault(pl => pl.PostID == postId && pl.UserID == userId);
            ỉtem.IsLike = isLike;
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
    }
}
