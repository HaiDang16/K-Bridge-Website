using K_Bridge.Models;

namespace K_Bridge.Repositories
{
    public class EFVoteRepository : IVoteRepository
    {
        private KBridgeDbContext _context;
        public EFVoteRepository(KBridgeDbContext context)
        {
            _context = context;
        }
        public IQueryable<Vote> Votes => _context.Votes;
        public void SaveVote(Vote vote)
        {
            if (vote.ID == 0)
                _context.Votes.Add(vote);
            _context.SaveChanges();
        }

    }
}
