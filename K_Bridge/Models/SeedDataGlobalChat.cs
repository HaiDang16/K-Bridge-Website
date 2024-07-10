using Microsoft.EntityFrameworkCore;

namespace K_Bridge.Models
{
    public class SeedDataGlobalChat
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            KBridgeDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<KBridgeDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.GlobalChats.Any())
            {
                context.GlobalChats.AddRange(
                new GlobalChat
                {
                    Username = "annguyen",
                    Content = "Chia sẻ bài tập Toán đây"
                },
                new GlobalChat
                {
                    Username = "abc123",
                    Content = "Có bạn nào biết cách làm mục lục tự động trong Word không chỉ mình với !!!!!!! Rất gấp !!!!!! Cầu cứu !!!!"
                },
                new GlobalChat
                {
                    Username = "annguyen",
                    Content = "Chia sẻ bài tập Toán đây"
                },
                new GlobalChat
                {
                    Username = "namanh",
                    Content = "Sắp tới có các hoạt động để kiếm điểm rèn luyện nào vậy mọi người ?"
                },
                new GlobalChat
                {
                    Username = "namnaam",
                    Content = "Nên dùng ASP.NET core phiên bản nào vậy mọi người"
                },
                new GlobalChat
                {
                    Username = "abc123",
                    Content = "Có bạn nào biết cách làm mục lục tự động trong Word không chỉ mình với !!!!!!! Rất gấp !!!!!! Cầu cứu !!!!"
                },
                new GlobalChat
                {
                    Username = "banban",
                    Content = "Ai muốn mua share tài khoản ChatGPT 4.0 không ạ ?"
                },
                new GlobalChat
                {
                    Username = "namanh",
                    Content = "Sắp tới có các hoạt động để kiếm điểm rèn luyện nào vậy mọi người ?"
                },
                new GlobalChat
                {
                    Username = "annguyen",
                    Content = "Chia sẻ bài tập Toán đây"
                }
                );
                context.SaveChanges();
            }
        }

    }
}
