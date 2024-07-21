using K_Bridge.Models;

namespace K_Bridge.Repositories
{
    public interface ILikeRepository
    {
        IQueryable<Post_Like> Post_Likes { get; }
        void SavePostLike(Post_Like like);
        Post_Like GetExistPostLike(int postId, int userId);
        void UpdateLike(Post_Like postLike, bool isLike);
        int GetLikeCount(int postId);
        int GetDislikeCount(int postId);
        void DeleteLike(int postId, int userId);
        void DeleteExistPostLike(Post_Like postLike);
    }
}
