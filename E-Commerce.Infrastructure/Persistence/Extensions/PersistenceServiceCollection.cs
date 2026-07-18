using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Features.Catalog.CategoryFeature.Interfaces;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Interfaces;
using E_Commerce.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Persistence.Extensions
{

    /*

        Persistence

    Everything related to data lives here:

    ApplicationDbContext
    Repositories
    Unit of Work
    EF Core configuration
    Database registration
     */
    public static class PersistenceServiceCollection
    {
        public static IServiceCollection AddPersistenceServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // register ApplicationDbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            // register repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            // register Unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            return services;
        }
    }
}
