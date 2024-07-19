using K_Bridge.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
