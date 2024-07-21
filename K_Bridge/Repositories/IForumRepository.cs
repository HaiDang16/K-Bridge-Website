using K_Bridge.Models;

namespace K_Bridge.Repositories
{
    public interface IForumRepository
    {
        IQueryable<Forum> Forums { get; }
        List<Forum> GetForumWithTopics();
        List<Forum> GetForumsWithTopicsAndLatestPosts();
        Forum GetTopicWithForumById(int id);
    }
}
