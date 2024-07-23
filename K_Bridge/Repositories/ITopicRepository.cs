using K_Bridge.Models;

namespace K_Bridge.Repositories
{
    public interface ITopicRepository
    {
        IQueryable<Topic> Topics { get; }
        List<Topic> GetAllTopics();
        Topic GetTopicById(int id);
        void SaveTopic(Topic topic);
        bool TopicNameExists(string name);
        void UpdateTopic(Topic topic);
        void SetTopicStatusInactive(int id);
        List<Topic> GetAllTopicsWithForumPaging(int forumID, int pageIndex, int pageSize);
        int CountTopicWithForum(int forumID);
        void SetTopicStatusActive(int id);
    }
}
