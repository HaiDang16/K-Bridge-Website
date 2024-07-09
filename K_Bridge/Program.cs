using K_Bridge.Models;
using K_Bridge.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<KBridgeDbContext>(opts => {
    opts.UseSqlServer(
    builder.Configuration["ConnectionStrings:KBridgeConnection"]);
});
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
}

// Đăng ký Repository
builder.Services.AddScoped<IKBridgeRepository, EFKBridgeRepository>();
builder.Services.AddScoped<IUserRepository, EFUserRepository>();
builder.Services.AddScoped<CodeGenerationService>();

builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();


var app = builder.Build(); 

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapDefaultControllerRoute();
app.MapRazorPages();

// Khởi tạo seed data
SeedData.EnsurePopulated(app);
SeedDataStats.EnsurePopulated(app);
SeedDataAdminAccount.EnsurePopulated(app);

app.Run();
