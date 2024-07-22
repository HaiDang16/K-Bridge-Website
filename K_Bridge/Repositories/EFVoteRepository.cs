using K_Bridge.Models;
using Microsoft.EntityFrameworkCore;

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
        public Vote GetVoteById(int id)
        {
            return _context.Votes
                 .Include(v => v.VoteOptions)
                .FirstOrDefault(p => p.ID == id);
        }

        public List<UserVote>? GetUserVotesListById(int id, Vote vote)
        {
            // First, get the IDs of the vote options
            var voteOptionIds = vote.VoteOptions.Select(vo => vo.ID).ToList();

            // Then, use these IDs in the query
            return _context.UserVotes
                .Where(v => v.UserID == id && voteOptionIds.Contains(v.VoteOptionID))
                .ToList();

        }
        public void RemoveAllVote(List<UserVote> vote)
        {
            _context.UserVotes.RemoveRange(vote);
        }
        public VoteOption GetVoteOptionById(int id)
        {
            return _context.VoteOptions.Find(id);
        }
        public void SaveUserVote(UserVote vote)
        {
            _context.UserVotes.Add(vote);
            _context.SaveChanges();
        }
        public void IncreaseOneVoteCount(VoteOption vote)
        {
            vote.Quantity++;
            _context.SaveChanges();
        }

        public List<VoteOption> GetVoteOptionsByVoteId(int voteId)
        {
            return _context.VoteOptions
                .Where(vo => vo.VoteID == voteId)
                .ToList();
        }
        public List<Vote> GetVotesByPostId(int postId)
        {
            return _context.Votes
                           .Where(v => v.PostID == postId)
                           .ToList();
        }
    }
}
