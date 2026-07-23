using E_Commerce.Application.Features.Cataglog.Product;
using E_Commerce.Application.Features.Catalog.Reviews;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Features.Cart_Feature;
using E_Commerce.Domain.Features.Catalog.BrandFeature.Interfaces;
using E_Commerce.Domain.Features.Catalog.CategoryFeature.Interfaces;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Interfaces;
using E_Commerce.Domain.Features.Catalog.ReviewFeature.Interfaces;
using E_Commerce.Domain.Features.WishlistFeature.Interfaces;
using E_Commerce.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IReviewReadRepository ,  ReviewReadRepository>();
            services.AddScoped<IWishlistRepository, WishlistRepository>();
            services.AddScoped<ICartRepository, CartRepository>();

            // register Unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            return services;
        }
    }
}
