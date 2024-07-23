using K_Bridge.Models;

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


    }
}
