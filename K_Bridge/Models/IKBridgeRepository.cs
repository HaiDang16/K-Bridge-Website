using K_Bridge.Models;

namespace K_Bridge.Models
{
    public interface IKBridgeRepository
    {
        IQueryable<Category> Categories { get; }
        IQueryable<Stats> Statses { get; }
        IQueryable<User> Users { get; }
    }
}