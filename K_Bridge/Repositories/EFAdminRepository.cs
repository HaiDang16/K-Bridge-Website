using K_Bridge.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace K_Bridge.Repositories
{
    public class EFAdminRepository : IAdminRepository
    {
        private readonly KBridgeDbContext _context;

        public EFAdminRepository(KBridgeDbContext context)
        {
            _context = context;
        }

        public IQueryable<Admin_Accounts> Admins => _context.Admin_Accounts;

        public async Task<Admin_Accounts> AuthenticateAdmin(string username, string password)
        {
            return await _context.Admin_Accounts
                .Where(a => a.Username == username && a.Password == password)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAdmin(Admin_Accounts admin)
        {
            if (admin.ID == 0)
                _context.Admin_Accounts.Add(admin);
            else
                _context.Admin_Accounts.Update(admin);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult > 0;
        }

        // Thêm các phương thức khác nếu cần
    }
}
