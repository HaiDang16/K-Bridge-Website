using K_Bridge.Models;
using Microsoft.EntityFrameworkCore;
using K_Bridge.Infrastructure;

namespace K_Bridge.Repositories
{
    public class EFUserRepository : IUserRepository
    {
        private KBridgeDbContext _context;
        public EFUserRepository(KBridgeDbContext context)
        {
            _context = context;
        }
        public IQueryable<User> Users => _context.Users;
        public void SaveUser(User user)
        {
            // Kiểm tra xem có id này chưa
            if (user.ID == 0)
                _context.Users.Add(user);
            _context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
        public int CountUser()
        {
            return _context.Users.Count();
        }
        public List<User> GetAllUsersPaging(int pageIndex, int pageSize)
        {
            return _context.Users
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
        }
        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(p => p.ID == id);
        }
        public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public bool DeleteUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
                return false;

            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        public void UpdateUserClient(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        public User GetUserWithPostById(int id)
        {
            return _context.Users
        .Include(u => u.Posts)
        .FirstOrDefault(u => u.ID == id);
        }
        public void IncreaseUserReputation(int userId, int increment)
        {
            var user = _context.Users.FirstOrDefault(u => u.ID == userId);
            if (user != null)
            {
                user.Reputation += increment;
                _context.SaveChanges();
            }
        }
    }
}
