using Microsoft.AspNetCore.Authentication.Cookies;
using PsychoShop.Framework.Application;
using PsychoShop.Framework.Application.AuthHelper;
using PsychoShop.Framework.Application.Email;
using PsychoShop.Framework.Application.PasswordHasher;
using PsychoShop.Infrastructure.Configuration;
using ServiceHost;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PsychoShopDB");
PsychoShopBootstrapper.Configure(builder.Services, connectionString);

builder.Services.AddTransient<IAuthHelper, AuthHelper>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
        options.Cookie.Name = "AuthIdentity";
        options.LoginPath = "/Account/SignIn";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapAreaControllerRoute(
            name: "admin",
            areaName: "Admin",
            pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
