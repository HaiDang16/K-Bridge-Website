using K_Bridge.Models;

namespace K_Bridge.Repositories
{
    public interface IAdminRepository
    {
        IQueryable<Admin_Accounts> Admins { get; }
        Task<Admin_Accounts> AuthenticateAdmin(string username, string password);
        Task<bool> SaveAdmin(Admin_Accounts admin);
    }
}
