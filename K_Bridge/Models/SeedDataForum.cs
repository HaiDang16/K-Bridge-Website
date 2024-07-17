using Microsoft.EntityFrameworkCore;

namespace K_Bridge.Models
{
    public class SeedDataForum
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            KBridgeDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<KBridgeDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Forums.Any())
            {
                context.Forums.AddRange(
                new Forum
                {
                    Name = "Lập trình",
                    IconLink = "Cate_Code.png",
                    TagName = "Lập trình"
                },
                new Forum
                {
                    Name = "Hướng dẫn",
                    IconLink = "Cate_Guide.png",
                    TagName = "Hướng dẫn",
                },
                new Forum
                {
                    Name = "Mẫu báo cáo, luận án",
                    IconLink = "Cate_Template.png",
                    TagName = "Mẫu báo cáo",
                },
                new Forum
                {
                    Name = "Tài liệu tổng hợp",
                    IconLink = "Cate_Document.png",
                    TagName = "Tài liệu",
                },
                new Forum
                {
                    Name = "Bài giảng trên lớp",
                    IconLink = "Cate_LessionVideo.png",
                    TagName = "Bài giảng",
                },
                new Forum
                {
                    Name = "Các hoạt động lấy điểm rèn luyện",
                    IconLink = "Cate_Activity.png",
                    TagName = "Điểm rèn luyện",
                }
                );
                context.SaveChanges();
            }
        }
    }
}
