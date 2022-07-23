using ServiceHost;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using PsychoShop.Framework.Application;
using PsychoShop.Framework.Application.Email;
using PsychoShop.Infrastructure.Configuration;
using PsychoShop.Framework.Application.AuthHelper;
using PsychoShop.Application.Contracts.UserClaim;
using PsychoShop.Framework.Application.ZarinPal;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PsychoShopDB");
PsychoShopBootstrapper.Configure(builder.Services, connectionString);

builder.Services.AddTransient<IAuthHelper, AuthHelper>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddTransient<IZarinPalFactory, ZarinPalFactory>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("CommentPolicy", policy => policy.RequireClaim(ClaimTypesStore.CommentManagement));
    x.AddPolicy("DiscountPolicy", policy => policy.RequireClaim(ClaimTypesStore.DiscountManagement));
    x.AddPolicy("HomePolicy", policy => policy.RequireClaim(ClaimTypesStore.HomeManagement));
    x.AddPolicy("InventoryPolicy", policy => policy.RequireClaim(ClaimTypesStore.InventoryManagement));
    x.AddPolicy("ProductCategoryPolicy", policy => policy.RequireClaim(ClaimTypesStore.ProductCategoryManagement));
    x.AddPolicy("ProductSubCategoryPolicy", policy => policy.RequireClaim(ClaimTypesStore.ProductSubCategoryManagement));
    x.AddPolicy("ProductPolicy", policy => policy.RequireClaim(ClaimTypesStore.ProductManagement));
    x.AddPolicy("ProductPicturePolicy", policy => policy.RequireClaim(ClaimTypesStore.ProductPictureManagement));
    x.AddPolicy("SpecialProductPolicy", policy => policy.RequireClaim(ClaimTypesStore.SpecialProductManagement));
    x.AddPolicy("UserAccountPolicy", policy => policy.RequireClaim(ClaimTypesStore.UserAccountManagement));
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

app.UseAuthentication();
app.UseAuthorization();

app.MapAreaControllerRoute(
            name: "admin",
            areaName: "Admin",
            pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
