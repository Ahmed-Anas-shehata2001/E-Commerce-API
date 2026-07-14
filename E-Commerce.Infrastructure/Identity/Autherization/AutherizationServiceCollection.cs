using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Infrastructure.Identity.Autherization
{
    public static class AutherizationServiceCollection
    {
        public static IServiceCollection AddAuthorizationServices(
     this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                // Policies
                options.AddPolicy("AdminOnly", policy =>
                {
                    policy.RequireRole("Admin");
                });

                options.AddPolicy("ManagerOnly", policy =>
                {
                    policy.RequireRole("Manager");
                });

                options.AddPolicy("Employee", policy =>
                {
                    policy.RequireClaim("Department", "IT");
                });


            });



            return services;
        }

    }
}
