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
    }
}
