namespace K_Bridge.Models
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
