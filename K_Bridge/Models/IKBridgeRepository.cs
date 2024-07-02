using K_Bridge.Models;

namespace K_Bridge.Models
{
    public interface IKBridgeRepository
    {
        IQueryable<Category> Categories { get; }
    }
}