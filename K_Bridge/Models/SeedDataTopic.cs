using K_Bridge.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace K_Bridge.Models
{
    public class SeedDataTopic
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            KBridgeDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider.GetRequiredService<KBridgeDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Topics.Any())
            {
                context.Topics.AddRange(
                new Topic
                {
                    Name = "Xin abcbabc abc chủ đề 1",
                    CateId = 1
                },
                new Topic
                {
                    Name = "Xin abcbabc abc chủ đề 1",
                    CateId = 1
                },
                new Topic
                {
                    Name = "Xin abcbabc abc chủ đề 2",
                    CateId = 2
                },
                new Topic
                {
                    Name = "Xin abcbabc abc chủ đề 2",
                    CateId = 2
                },
                new Topic
                {
                    Name = "Xin abcbabc abc chủ đề 3",
                    CateId = 3
                },
                new Topic
                {
                    Name = "Xin abcbabc abc chủ đề 3",
                    CateId = 3
                }
                );
                context.SaveChanges();
            }
        }
    }
}
