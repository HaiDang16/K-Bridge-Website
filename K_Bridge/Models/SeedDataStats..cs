using K_Bridge.Models;
using Microsoft.EntityFrameworkCore;

namespace K_Bridge.Models
{
    public class SeedDataStats
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            KBridgeDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider.GetRequiredService<KBridgeDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Statses.Any())
            {
                context.Statses.AddRange(
                new Stats
                {
                    Name = "Chủ đề",
                    Count = 0,
                    LinkIcon = "Stats_Topic.png",
                },

                new Stats
                {
                    Name = "Bài viết",
                    Count = 0,
                    LinkIcon = "Stats_Post.png",
                },
                new Stats
                {
                    Name = "Thành viên",
                    Count = 0,
                    LinkIcon = "Stats_Account.png",
                }
                );
                context.SaveChanges();
            }
        }
    }
}
