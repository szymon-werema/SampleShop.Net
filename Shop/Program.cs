using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Entities;
using Shop.Models.Register;
using Shop.Models.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Shop.Models.Messenger;
using Shop.Models.Authenticate;
using Shop.Models.AccountMenager;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Shop.Models.Configs;
using Shop.Models.Forms;

var builder = WebApplication.CreateBuilder(args);

var jwtConfig = builder.Configuration.GetSection("Authentication").Get<JwtSetting>();
var emailConfig = builder.Configuration.GetSection("MailSettings").Get<EmailConfig>();


builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer( cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.JwtKey)),
        
       
    };
});
builder.Services.AddSingleton(jwtConfig);
builder.Services.AddSingleton(emailConfig);
builder.Services.AddControllersWithViews();
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddDbContext<LocalDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("LocalDatabase"))
    );

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();



//Validators
builder.Services.AddScoped<IValidator<SetPasswordForm>, SetPasswordValidator>();
builder.Services.AddScoped<IValidator<UserRegisterForm>, RegisterValidator>();
builder.Services.AddScoped<IValidator<AddUserForm>, AddUserValidator>();

//Messnger
builder.Services.AddScoped<IMessenger<EmailMessageActivation>, EmailMessageActivation>();

//Tokens
builder.Services.AddScoped<IToken<TokenByAdmin> , TokenByAdmin>();
builder.Services.AddScoped<IToken<TokenRegister> , TokenRegister>();
builder.Services.AddScoped<AccountMenager>();

//Claims
builder.Services.AddScoped<IClaimsUser<ClaimsRegister>, ClaimsRegister>();
builder.Services.AddScoped<IClaimsUser<ClaimsAddByAdmin>, ClaimsAddByAdmin>();

//Register
builder.Services.AddScoped<IRegister<UserRegisterForm> , RegisterClient>();
builder.Services.AddScoped<IRegister<AddUserForm>, RegisterByAdmin>();

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

app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



