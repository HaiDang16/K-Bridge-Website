using K_Bridge.Models;

namespace K_Bridge.Repositories
{
    public interface IAdminAccountRepository
    {
        IQueryable<Admin_Accounts> Admin_Accounts { get; }
        void SaveAdminAccount(Admin_Accounts account);
        int CountAdminAccount();
        List<Admin_Accounts> GetAllAdminAccountsPaging(int pageIndex, int pageSize);
        Admin_Accounts GetAdminAccountById(int id);
        void SetAdminAccountStatusInactive(int id);
    }
}
