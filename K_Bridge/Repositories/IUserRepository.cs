using K_Bridge.Models;

namespace K_Bridge.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        void SaveUser(User user);
    }
}
