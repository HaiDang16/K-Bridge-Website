using K_Bridge.Models;

namespace K_Bridge.Repositories
{
    public interface ITopicRepository
    {
        IQueryable<Topic> Topics{ get; }
        List<Topic> GetAllTopics();

    }
}
