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
        public IQueryable<Reply_Like> Reply_Likes => _context.Reply_Likes;

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
        public void UpdatePostLike(Post_Like postLike, bool isLike)
        {
            postLike.IsLike = isLike;
            _context.SaveChanges();
        }
        public int GetPostLikeCount(int postId)
        {
            return _context.Post_Likes.Count(pl => pl.PostID == postId && pl.IsLike);
        }
        public int GetPostDislikeCount(int postId)
        {
            return _context.Post_Likes.Count(pl => pl.PostID == postId && !pl.IsLike);
        }

        public void DeletePostLike(int postId, int userId)
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

        public void SaveReplyLike(Reply_Like reply)
        {
            if (reply.ID == 0)
                _context.Reply_Likes.Add(reply);
            _context.SaveChanges();
        }
        public Reply_Like GetExistReplyLike(int replyID, int userId)
        {
            return _context.Reply_Likes.FirstOrDefault(pl => pl.ReplyID == replyID && pl.UserID == userId);
        }
        public void UpdateReplyLike(Reply_Like reply, bool isLike)
        {
            reply.IsLike = isLike;
            _context.SaveChanges();
        }
        public int GetReplyLikeCount(int replyID)
        {
            return _context.Reply_Likes.Count(pl => pl.ReplyID == replyID && pl.IsLike);
        }

        public int GetReplyDislikeCount(int replyID)
        {
            return _context.Reply_Likes.Count(pl => pl.ReplyID == replyID && !pl.IsLike);
        }
        public void DeleteReplyLike(int replyID, int userId)
        {
            var like = _context.Reply_Likes.FirstOrDefault(pl => pl.ReplyID == replyID && pl.UserID == userId);
            if (like != null)
            {
                _context.Reply_Likes.Remove(like);
                _context.SaveChanges();
            }
        }
        public void DeleteExistReplyLike(Reply_Like reply)
        {
            _context.Reply_Likes.Remove(reply);
            _context.SaveChanges();
        }

        public int GetUserReplyLikeStatus(int replyId, int userId)
        {
            var like = _context.Reply_Likes.FirstOrDefault(l => l.ReplyID == replyId && l.UserID == userId);
            return like == null ? 0 : (like.IsLike ? 1 : -1);
        }
        public int ToggleReplyLike(int replyId, int userId, bool isLike)
        {
            var existingLike = _context.Reply_Likes.FirstOrDefault(l => l.ReplyID == replyId && l.UserID == userId);
            if (existingLike != null)
            {
                if (existingLike.IsLike == isLike)
                    return 0;
                else
                    return isLike ? 1 : -1;
            }
            else
                return isLike ? 1 : -1;
        }
    }
}
