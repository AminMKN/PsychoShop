﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PsychoShop.Application;
using PsychoShop.Application.Contracts.Account;
using PsychoShop.Application.Contracts.ProductCategory;
using PsychoShop.Domain.AccountAgg;
using PsychoShop.Domain.ProductCategoryAgg;
using PsychoShop.Infrastructure.EFCore;
using PsychoShop.Infrastructure.EFCore.Repository;

namespace PsychoShop.Infrastructure.Configuration
{
    public class PsychoShopBootstrapper
    {
        public static void Configure(IServiceCollection service, string connectionString)
        {
            service.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            service.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();

            service.AddTransient<IAccountRepository, AccountRepository>();
            service.AddTransient<IAccountApplication, AccountApplication>();

            service.AddDbContext<PsychoShopContext>(x => x.UseSqlServer(connectionString));
        }
    }
}