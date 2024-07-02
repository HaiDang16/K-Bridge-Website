using K_Bridge.Models;
using Microsoft.EntityFrameworkCore;

namespace K_Bridge.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            KBridgeDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider.GetRequiredService<KBridgeDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                new Category
                {
                    Name = "Lập trình",
                    TopicCount = 0,
                    LinkIcon = "Cate_Code.png"
                },
                new Category
                {
                    Name = "Hướng dẫn",
                    TopicCount = 0,
                    LinkIcon = "Cate_Guide.png"
                },
                new Category
                {
                    Name = "Mẫu báo cáo, luận án",
                    TopicCount = 0,
                    LinkIcon = "Cate_Template.png"
                },
                new Category
                {
                    Name = "Tài liệu tổng hợp",
                    TopicCount = 0,
                    LinkIcon = "Cate_Document.png"
                },
                new Category
                {
                    Name = "Bài giảng trên lớp",
                    TopicCount = 0,
                    LinkIcon = "Cate_LessionVideo.png"
                },
                new Category
                {
                    Name = "Các hoạt động lấy điểm rèn luyện",
                    TopicCount = 0,
                    LinkIcon = "Cate_Activity.png"
                }
                );
                context.SaveChanges();
            }
        }
    }
}
