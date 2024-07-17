using K_Bridge.Models;
using Microsoft.EntityFrameworkCore;

namespace K_Bridge.Repositories
{
    public class EFReplyRepository : IReplyRepository
    {
        private KBridgeDbContext _context;
        public EFReplyRepository(KBridgeDbContext context)
        {
            _context = context;
        }
        public IQueryable<Reply> Replies => _context.Replies;
        public void SaveReply(Reply reply)
        {
            // Kiểm tra xem có id này chưa
            if (reply.ID == 0)
                _context.Replies.Add(reply);
            _context.SaveChanges();
        }
        public IEnumerable<Reply> GetRepliesByPostId(int postId)
        {
            return _context.Replies
                           .Where(r => r.PostID == postId)
                           .Include(r => r.User)
                           .ToList();
        }
    }
}
