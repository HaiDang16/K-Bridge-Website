using K_Bridge.Models;
using Microsoft.EntityFrameworkCore.Migrations;

namespace K_Bridge.Models
{
    public class EFKBridgeRepository : IKBridgeRepository
    {
        private KBridgeDbContext context;
        public EFKBridgeRepository(KBridgeDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Category> Categories => context.Categories;
        public IQueryable<Stats> Statses => context.Statses;
        public IQueryable<User> Users => context.Users;
        public IQueryable<Topic> Topics => context.Topics;
    }

}
