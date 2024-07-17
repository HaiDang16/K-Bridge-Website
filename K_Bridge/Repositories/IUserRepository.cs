using K_Bridge.Models;
using Microsoft.EntityFrameworkCore;

namespace K_Bridge.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        List<User> GetAllUsers();
        void SaveUser(User user);
        int CountUser();
        List<User> GetAllUsersPaging(int pageIndex, int pageSize);
    }
}
