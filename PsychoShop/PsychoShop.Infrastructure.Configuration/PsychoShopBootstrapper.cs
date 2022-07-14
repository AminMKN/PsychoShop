﻿using PsychoShop.Application;
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

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductApplication, ProductApplication>();

            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();
            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();

            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();

            services.AddTransient<IProductSubCategoryRepository, ProductSubCategoryRepository>();
            services.AddTransient<IProductSubCategoryApplication, ProductSubCategoryApplication>();

            services.AddDbContext<PsychoShopContext>(x => x.UseSqlServer(connectionString));
        }
    }
}