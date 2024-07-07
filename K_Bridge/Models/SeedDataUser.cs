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
                    Username = "admintest",
                    Email = "admintest@gmail.com",
                    Password = "test1313",
                    //Roles = "Admin"
                },
                new User
                {
                    Username = "usertest",
                    Email = "usertest@gmail.com",
                    Password = "test1313",
                    //Roles = "User"
                },
                new User
                {
                    Username = "alltest",
                    Email = "alltest@gmail.com",
                    Password = "test1313",
                    //Roles = "Admin"+"User"
                }
                );
                context.SaveChanges();
            }
        }
    }
}
