using K_Bridge.Models;

namespace K_Bridge.Repositories
{
    public interface IAdminAccountRepository
    {
        IQueryable<Admin_Accounts> Admin_Accounts { get; }
        void SaveAdminAccount(Admin_Accounts account);

    }
}
