using K_Bridge.Models;
using Microsoft.EntityFrameworkCore;

namespace K_Bridge.Repositories
{
    public class EFTopicRepository : ITopicRepository
    {
        private KBridgeDbContext _context;
        public EFTopicRepository(KBridgeDbContext context)
        {
            _context = context;
        }
        public IQueryable<Topic> Topics => _context.Topics;
        public List<Topic> GetAllTopics()
        {
            return _context.Topics
                .ToList();
        }
        public Topic GetTopicById(int id)
        {
            return _context.Topics.FirstOrDefault(p => p.ID == id);
        }
        public void SaveTopic(Topic topic)
        {
            // Kiểm tra xem có id này chưa
            if (topic.ID == 0)
                _context.Topics.Add(topic);
            _context.SaveChanges();
        }
        public bool TopicNameExists(string name)
        {
            return _context.Topics.Any(t => t.Name == name);
        }
        public void UpdateTopic(Topic topic)
        {
            _context.Topics.Update(topic);
            _context.SaveChanges();
        }
        public void SetTopicStatusInactive(int id)
        {
            var topic = _context.Topics.Find(id);
            if (topic != null)
            {
                topic.Status = "Inactive";
                _context.Topics.Update(topic);
                _context.SaveChanges();
            }
        }
        public List<Topic> GetAllTopicsWithForumPaging(int forumID, int pageIndex, int pageSize)
        {
            return _context.Topics
                .Where(t => t.ForumID == forumID)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int CountTopicWithForum(int forumID)
        {
            return _context.Topics
                .Where(t => t.ForumID == forumID)
                .Count();
        }

        public void SetTopicStatusActive(int id)
        {
            var topic = _context.Topics.Find(id);
            if (topic != null)
            {
                topic.Status = "Active";
                _context.SaveChanges();
            }
        }

    }
}
