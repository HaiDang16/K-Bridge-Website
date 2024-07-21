using K_Bridge.Models;

namespace K_Bridge.Repositories
{
    public interface ILikeRepository
    {
        IQueryable<Post_Like> Post_Likes { get; }
        void SavePostLike(Post_Like like);
        Post_Like GetExistPostLike(int postId, int userId);
        void UpdatePostLike(Post_Like postLike, bool isLike);
        int GetPostLikeCount(int postId);
        int GetPostDislikeCount(int postId);
        void DeletePostLike(int postId, int userId);
        void DeleteExistPostLike(Post_Like postLike);

        void SaveReplyLike(Reply_Like like);
        Reply_Like GetExistReplyLike(int replyID, int userId);
        void UpdateReplyLike(Reply_Like reply, bool isLike);
        int GetReplyLikeCount(int replyID);
        int GetReplyDislikeCount(int replyID);
        void DeleteReplyLike(int replyID, int userId);
        void DeleteExistReplyLike(Reply_Like reply);
        int GetUserReplyLikeStatus(int replyId, int userId);
        int ToggleReplyLike(int replyId, int userId, bool isLike);
    }
}
