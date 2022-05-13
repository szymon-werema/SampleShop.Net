using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Entities;
using Shop.Models.Register;
using Shop.Models.Users;
using Shop.Models.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddDbContext<LocalDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("LocalDatabase"))
    );
builder.Services.AddScoped<IFactoryUser, FactoryUser>();
builder.Services.AddScoped<IRegister<UserAdmin>, RegisterAdmin>();
builder.Services.AddScoped<IRegister<UserSeller>, RegisterSeller>();
builder.Services.AddScoped<IRegister<UserClient>, RegisterClient>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<UserClient>, RegisterValidator>();
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
