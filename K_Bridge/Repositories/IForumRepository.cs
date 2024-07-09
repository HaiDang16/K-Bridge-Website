using K_Bridge.Models;

namespace K_Bridge.Repositories
{
    public interface IForumRepository
    {
        IQueryable<Forum> Forums { get; }
    }
}
