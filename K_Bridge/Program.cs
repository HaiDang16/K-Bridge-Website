using K_Bridge.Models;
using K_Bridge.Repositories;
using K_Bridge.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<KBridgeDbContext>(opts =>
{
    opts.UseSqlServer(
    builder.Configuration["ConnectionStrings:KBridgeConnection"]);
});

builder.Services.AddRazorPages();
/*builder.Services.AddRazorPages().AddRazorRuntimeCompilation();*/


// Đăng ký Repository
builder.Services.AddScoped<IKBridgeRepository, EFKBridgeRepository>();
builder.Services.AddScoped<IUserRepository, EFUserRepository>();
builder.Services.AddScoped<IForumRepository, EFForumRepository>();
builder.Services.AddScoped<IGlobalChatRepository, EFGlobalChatRepository>();
builder.Services.AddScoped<IPostRepository, EFPostRepository>();
builder.Services.AddScoped<ITopicRepository, EFTopicRepository>();
builder.Services.AddScoped<IReplyRepository, EFReplyRepository>();
builder.Services.AddScoped<ILikeRepository, EFLikeRepository>();

builder.Services.AddScoped<CodeGenerationService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Đảm bảo bạn có dòng này trong phần cấu hình middleware
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

app.MapRazorPages();
app.MapDefaultControllerRoute();
// Khởi tạo seed data
SeedDataAdminAccount.EnsurePopulated(app);
SeedDataForum.EnsurePopulated(app);
SeedDataGlobalChat.EnsurePopulated(app);
SeedDataTopic.EnsurePopulated(app);


app.Run();
