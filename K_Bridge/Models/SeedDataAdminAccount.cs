using Microsoft.EntityFrameworkCore;
namespace K_Bridge.Models
{
    public static class SeedDataAdminAccount
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            KBridgeDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider.GetRequiredService<KBridgeDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Admin_Accounts.Any())
            {
                context.Admin_Accounts.AddRange(
                new Admin_Accounts
                {
                    Username = "Admin1",
                    Email = "admin1@gmail.com",
                    Password = "123",
                    LastLogin = DateTime.Now,
                    Status = "Active",
                    Role = "Emp"
                },
                new Admin_Accounts
                {
                    Username = "Admin2",
                    Email = "admin2@gmail.com",
                    Password = "123",
                    LastLogin = DateTime.Now,
                    Status = "Active",
                    Role = "Mgr"
                },
                new Admin_Accounts
                {
                    Username = "Admin3",
                    Email = "admin4@gmail.com",
                    Password = "123123",
                    LastLogin = DateTime.Now,
                    Status = "Active",
                    Role = "Emp"
                }
                );
                context.SaveChanges();
            }
        }
    }
}//dotnet ef migrations add Initial