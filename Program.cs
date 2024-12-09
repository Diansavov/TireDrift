using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TireDrift.Data;
using TireDrift.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TiresDbContext>(options =>
             options.UseMySql(builder.Configuration.GetConnectionString("TireDriftConnectionString"), new MySqlServerVersion(new Version(10, 0, 1))));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().AddEntityFrameworkStores<TiresDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "UserCookie";
    options.LoginPath = "/Users/LogIn";
});
//Services

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
