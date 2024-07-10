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
    }
}