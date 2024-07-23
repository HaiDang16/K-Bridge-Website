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
        Post GetPostWithVoteById(int id);
        Reply GetReplyById(int id);
        void RemoveReply(Reply reply);
        IEnumerable<Post> GetPostsByTopicFilter(int topicId);
        IEnumerable<Post> PostsFilterTrending(IEnumerable<Post> posts);
        IEnumerable<Post> PostsFilterHelpful(IEnumerable<Post> posts);
        List<Post> GetAllPostsWithTopicPaging(int topicId, int pageIndex, int pageSize);
        int CountPostWithTopic(int topicId);
        void RemovePost(int id);

    }
}
