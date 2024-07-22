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
            _context.SaveChanges();

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
        public Vote GetVoteByPostId(int postId)
        {
            return _context.Votes.FirstOrDefault(v => v.PostID == postId);
        }
        public List<UserVote> GetUserVoteForPost(int userId, int postId)
        {
            return _context.UserVotes
           .Where(uv => uv.UserID == userId && uv.VoteOption.Vote.PostID == postId)
           .ToList();
        }
        public void RemoveUserVote(UserVote userVote)
        {
            // Check if the UserVote entity exists in the context
            var existingVote = _context.UserVotes.Find(userVote.ID);
            if (existingVote != null)
            {
                _context.UserVotes.Remove(existingVote);
                _context.SaveChanges();
            }
        }
        public void DecreaseOneVoteCount(VoteOption voteOption)
        {
            // Find the existing VoteOption entity
            var existingOption = _context.VoteOptions.Find(voteOption.ID);
            if (existingOption != null)
            {
                // Decrease the vote count if it's greater than 0
                if (existingOption.Quantity > 0)
                {
                    existingOption.Quantity--;
                    _context.VoteOptions.Update(existingOption);
                    _context.SaveChanges();
                }
            }
        }
        public Vote GetVoteWithOptionByPostId(int id)
        {
           return _context.Votes
                .Include(v => v.VoteOptions)
                .Where(v => v.PostID == id)
                .FirstOrDefault();
        }

        public int CountUserVoteById(int id)
        {
            return _context.UserVotes
                    .Count(uv => uv.VoteOptionID == id);
        }
    }
}
