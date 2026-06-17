using E_Commerce.Application.Common.Interfaces;
using E_Commerce.Domain.Features.CategoryFeature.Interfaces;
using E_Commerce.Domain.Features.ProductFeature.Interfaces;
using E_Commerce.Infrastructure.Persistence;
using E_Commerce.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // ✅ DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            // ✅ Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            // Unit of Work (MISSING PIECE)
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }


    }
}
