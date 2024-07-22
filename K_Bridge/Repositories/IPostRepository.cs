using K_Bridge.Models;

namespace K_Bridge.Repositories
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts { get; }
        void SavePost(Post post);
        List<Post> GetLatestPost();
        Post GetPostByID(int id);
        void IncrementViewCount(int postId);
        List<Post> GetPostsByTopic(int topicId);
        void UpdatePost(Post post);
    }
}
