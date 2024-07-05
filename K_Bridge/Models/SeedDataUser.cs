using K_Bridge.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace K_Bridge.Models
{
    public class SeedDataUser
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            KBridgeDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider.GetRequiredService<KBridgeDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                new User
                {
                    UserName = "admintest",
                    Email = "admintest@gmail.com",
                    Password = "test1313",
                    Roles = "Admin"
                },
                new User
                {
                    UserName = "usertest",
                    Email = "usertest@gmail.com",
                    Password = "test1313",
                    Roles = "User"
                },
                new User
                {
                    UserName = "alltest",
                    Email = "alltest@gmail.com",
                    Password = "test1313",
                    Roles = "Admin"+"User"
                }
                );
                context.SaveChanges();
            }
        }
    }
}
