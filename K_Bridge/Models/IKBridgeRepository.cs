using K_Bridge.Models;

namespace K_Bridge.Models
{
    // Đóng vai trò cung cấp dữ liệu cho trang web
    public interface IKBridgeRepository
    {
        IQueryable<Category> Categories { get; }
        IQueryable<Stats> Statses { get; }
        IQueryable<User> Users { get; }
        IQueryable<Topic> Topics { get; }
        IQueryable<Admin_Accounts> Admin_Accounts { get; }
    }
}