using K_Bridge.Models;
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
builder.Services.AddScoped<IKBridgeRepository, EFKBridgeRepository>();

var app = builder.Build(); 

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
SeedData.EnsurePopulated(app);
SeedDataStats.EnsurePopulated(app);
app.Run();
