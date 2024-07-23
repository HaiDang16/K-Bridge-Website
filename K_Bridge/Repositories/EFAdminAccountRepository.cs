using K_Bridge.Models;
using Microsoft.EntityFrameworkCore;

namespace K_Bridge.Repositories
{
    public class EFAdminAccountRepository : IAdminAccountRepository
    {
        private KBridgeDbContext _context;
        public EFAdminAccountRepository(KBridgeDbContext context)
        {
            _context = context;
        }
        public IQueryable<Admin_Accounts> Admin_Accounts => _context.Admin_Accounts;
        public void SaveAdminAccount(Admin_Accounts account)
        {
            if (account.ID == 0)
                _context.Admin_Accounts.Add(account);
            _context.SaveChanges();
        }

        public int CountAdminAccount()
        {
            return _context.Admin_Accounts.Count();
        }
        public List<Admin_Accounts> GetAllAdminAccountsPaging(int pageIndex, int pageSize)
        {
            return _context.Admin_Accounts
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
        }
        public Admin_Accounts GetAdminAccountById(int id)
        {
            return _context.Admin_Accounts.FirstOrDefault(p => p.ID == id);
        }
        public void SetAdminAccountStatusInactive(int id)
        {
            var admin = _context.Admin_Accounts.Find(id);
            if (admin != null)
            {
                admin.Status = "Inactive";
                _context.Admin_Accounts.Update(admin);
                _context.SaveChanges();
            }
        }
    }
}
