using K_Bridge.Repositories;

namespace K_Bridge.Services
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;

        public VoteService(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        public void UpdateVoteStatus()
        {
            var openVotes = _voteRepository.GetOpenVotes();
            var votesToClose = openVotes.Where(v => DateTime.UtcNow > v.GetCloseTime()).ToList();

            foreach (var vote in votesToClose)
            {
                vote.Status = "Close";
                _voteRepository.UpdateVote(vote);
            }
        }
    }
}