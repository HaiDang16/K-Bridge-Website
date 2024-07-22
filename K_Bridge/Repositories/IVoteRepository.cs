using K_Bridge.Models;

namespace K_Bridge.Repositories
{
    public interface IVoteRepository
    {
        IQueryable<Vote> Votes{ get; }
        void SaveVote(Vote vote);

    }
}
