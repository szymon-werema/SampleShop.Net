using Microsoft.EntityFrameworkCore;
using Shop;
using Shop.Entities;
using Shop.Models;
using Shop.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<LocalDBContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("LocalDatabase"))
    );
builder.Services.AddScoped<RegisterAdmin>();


builder.Services.AddScoped <IUserFactory, UserFactory>();
builder.Services.AddScoped <IRegister<UserAccountAdmin>, RegisterAdmin>();
builder.Services.AddScoped <IRegister<UserAccountUser>, RegisterNormalUser>();

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

app.Run();
