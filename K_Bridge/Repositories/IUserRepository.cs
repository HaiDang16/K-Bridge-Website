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
        User GetUserById(int id);
        void UpdateUser(User user);
        bool DeleteUser(int id);
        void UpdateUserClient(User user);
        User GetUserWithPostById(int id);
        void IncreaseUserReputation(int userId, int increment);

    }
}
