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
    }
}
