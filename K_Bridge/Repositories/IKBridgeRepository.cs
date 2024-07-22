using K_Bridge.Models;

namespace K_Bridge.Repositories
{
    // Đóng vai trò cung cấp dữ liệu cho trang web
    public interface IKBridgeRepository
    {
        IQueryable<User> Users { get; }
        IQueryable<Topic> Topics { get; }
        IQueryable<Admin_Accounts> Admin_Accounts { get; }
        IQueryable<Post> Posts { get; }
        IQueryable<Forum> Forums { get; }

        int GetTotalUsers();
        int GetTotalTopics();
        int GetTotalPosts();

        int GetRecentUsers();
        int GetRecentTopics();
        int GetRecentPosts();
    }
}