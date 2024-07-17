using K_Bridge.Models;

namespace K_Bridge.Repositories
{
    public interface ILikeRepository
    {
        IQueryable<Post_Like> Post_Likes { get; }
        void SavePostLike(Post_Like like);
        Post_Like GetPostLike(int postId, int userId);
        void UpdateLike(int postId, int userId, bool isLike);
        int GetLikeCount(int postId);
        int GetDislikeCount(int postId);
        void DeleteLike(int postId, int userId);
    }
}
