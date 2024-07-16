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
                    Name = "Lập trình Java",
                    Description = "Hướng dẫn lập trình bằng ngôn ngữ Java",
                    ForumID = 1,
                    Status = "Active"
                },
                new Topic
                {
                    Name = "Lập trình Python",
                    Description = "Hướng dẫn lập trình bằng ngôn ngữ Python",
                    ForumID = 2,
                    Status = "Active"
                },
                new Topic
                {
                    Name = "Chia sẻ đề cương thi",
                    Description = "Chia sẻ tài liệu học tập",
                    ForumID = 3,
                    Status = "Active"
                } 
                );
                context.SaveChanges();
            }
        }
    }
}
