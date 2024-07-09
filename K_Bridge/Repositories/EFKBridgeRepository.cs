using K_Bridge.Models;
using Microsoft.EntityFrameworkCore.Migrations;

namespace K_Bridge.Repositories
{
    public class EFKBridgeRepository : IKBridgeRepository
    {
        private KBridgeDbContext _context;
        public EFKBridgeRepository(KBridgeDbContext ctx)
        {
            _context = ctx;
        }
        public IQueryable<Category> Categories => _context.Categories;
        public IQueryable<Stats> Statses => _context.Statses;
        public IQueryable<User> Users => _context.Users;
        public IQueryable<Topic> Topics => _context.Topics;
        public IQueryable<Admin_Accounts> Admin_Accounts => _context.Admin_Accounts;
    }

}
