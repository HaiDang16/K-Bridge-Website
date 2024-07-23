using K_Bridge.Models;

namespace K_Bridge.Repositories
{
    public class EFGlobalChatRepository : IGlobalChatRepository
    {
        private KBridgeDbContext _context;
        public EFGlobalChatRepository(KBridgeDbContext context)
        {
            _context = context;
        }
        public IQueryable<GlobalChat> GlobalChats => _context.GlobalChats;
        public IEnumerable<GlobalChat> GetRecentMessages()
        {
            DeleteOldMessages(DateTime.Now.AddHours(-100)); // Xoá tin nhắn cũ hơn 1 giờ
            return _context.GlobalChats.OrderByDescending(m => m.SendAt);
        }

        public void AddMessage(GlobalChat message)
        {
            _context.GlobalChats.Add(message);
            _context.SaveChanges();
        }

        public void DeleteOldMessages(DateTime threshold)
        {
            var oldMessages = _context.GlobalChats.Where(m => m.SendAt < threshold);
            _context.GlobalChats.RemoveRange(oldMessages);
            _context.SaveChanges();
        }
    }
}