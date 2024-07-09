using K_Bridge.Models;

namespace K_Bridge.Repositories
{
    public class EFForumRepository : IForumRepository
    {
        private KBridgeDbContext _context;
        public EFForumRepository(KBridgeDbContext context)
        {
            _context = context;
        }
        public IQueryable<Forum> Forums => _context.Forums;
    }
}
