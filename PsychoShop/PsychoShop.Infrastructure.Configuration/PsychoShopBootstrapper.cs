using PsychoShop.Application;
using PsychoShop.Domain.UserClaim;
using PsychoShop.Domain.ProductAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using PsychoShop.Domain.UserAccountAgg;
using PsychoShop.Framework.Application;
using PsychoShop.Infrastructure.EFCore;
using PsychoShop.Domain.ProductCategoryAgg;
using PsychoShop.Domain.ProductSubCategoryAgg;
using Microsoft.Extensions.DependencyInjection;
using PsychoShop.Application.Contracts.Product;
using PsychoShop.Application.Contracts.UserClaim;
using PsychoShop.Application.Contracts.UserAccount;
using PsychoShop.Application.Contracts.ProductCategory;
using PsychoShop.Application.Contracts.ProductSubCategory;
using PsychoShop.Infrastructure.EFCore.Repository;
using PsychoShop.Domain.ProductPictureAgg;
using PsychoShop.Application.Contracts.ProductPicture;
using PsychoShop.Domain.SpecialProductAgg;
using PsychoShop.Application.Contracts.SpecialProduct;
using PsychoShop.Domain.DiscountAgg;
using PsychoShop.Application.Contracts.Discount;
using PsychoShop.Domain.InventoryAgg;
using PsychoShop.Application.Contracts.Inventory;
using PsychoShop.Query.Query;
using PsychoShop.Query.Contract.ProductSubCategory;
using PsychoShop.Query.Contract.ProductCategory;
using PsychoShop.Query.Contract.Product;
using PsychoShop.Application.Contracts.Comment;
using PsychoShop.Domain.CommentAgg;
using PsychoShop.Domain.OrderAgg;
using PsychoShop.Application.Contracts.Order;
using PsychoShop.Domain.Services;
using PsychoShop.Application.Contracts.ShopCart;
using PsychoShop.Query.Contract;

namespace PsychoShop.Infrastructure.Configuration
{
    public class PsychoShopBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services
                .AddIdentity<UserAccount, IdentityRole>(option =>
                {
                    option.Password.RequiredLength = 6;
                    option.Password.RequiredUniqueChars = 0;
                    option.Password.RequireDigit = false;
                    option.Password.RequireNonAlphanumeric = false;
                    option.User.RequireUniqueEmail = true;
                    option.SignIn.RequireConfirmedEmail = false;
                })
                .AddEntityFrameworkStores<PsychoShopContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<PersianIdentityErrorDescriber>();

            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.Cookie.Name = "AuthIdentity";
                options.LoginPath = "/UserAccount/SignIn";
                options.AccessDeniedPath = "/UserAccount/AccessDenied";
            });

            services.AddTransient<IUserAccountRepository, UserAccountRepository>();
            services.AddTransient<IUserAccountApplication, UserAccountApplication>();

            services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            services.AddTransient<IUserClaimApplication, UserClaimApplication>();

            services.AddTransient<IInventoryRepository, InventoryRepository>();
            services.AddTransient<IInventoryApplication, InventoryApplication>();

            services.AddTransient<IDiscountRepository, DiscountRepository>();
            services.AddTransient<IDiscountApplication, DiscountApplication>();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductApplication, ProductApplication>();

            services.AddTransient<ISpecialProductRepository, SpecialProductRepository>();
            services.AddTransient<ISpecialProductApplication, SpecialProductApplication>();

            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();
            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();

            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();

            services.AddTransient<IProductSubCategoryRepository, ProductSubCategoryRepository>();
            services.AddTransient<IProductSubCategoryApplication, ProductSubCategoryApplication>();

            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<ICommentApplication, CommentApplication>();

            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderApplication, OrderApplication>();

            services.AddSingleton<ICartService, CartService>();

            services.AddTransient<IShopInventoryAcl, ShopInventoryAcl>();

            services.AddTransient<ICartCalculatorService, CartCalculatorService>();

            services.AddTransient<IProductQuery, ProductQuery>();

            services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();

            services.AddTransient<IProductSubCategoryQuery, ProductSubCategoryQuery>();

            services.AddDbContext<PsychoShopContext>(x => x.UseSqlServer(connectionString));
        }
    }
}