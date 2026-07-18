using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Infrastructure.Identity.Autherization;
using E_Commerce.Infrastructure.Identity.Extensions;
using E_Commerce.Infrastructure.Identity.JWT;
using E_Commerce.Infrastructure.Identity.Services;
using E_Commerce.Infrastructure.Identity.Services.SendGrid;
using E_Commerce.Infrastructure.Persistence.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SendGrid;
using UAParser;

namespace E_Commerce.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {


            // register DbContext  , Repositoreis  , unit of work  ( ** Data ** )
            services.AddPersistenceServices(configuration);

            // resister identity
            services.AddIdentityServices(configuration);
            // register JWT
            services.AddJwtAuthentication(configuration);
            // register Autherization
            services.AddAuthorizationServices();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();


            //services.AddScoped<IPermissionService, PermissionService>();


            // register configuraiton like JWTSettings  ( options pattern ) 
            services.Configure<JWTSettings>(
         configuration.GetSection("JWTSettings"));



            // register SendGridSettings 
            services.Configure<SendGridSettings>(configuration.GetSection("SendGrid"));
            services.AddSingleton<ISendGridClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<SendGridSettings>>().Value;

                return new SendGridClient(settings.ApiKey);
            });
            services.AddScoped<IEmailSender, SendGridEmailService>();


            // register HttpContext Accessor
            services.AddHttpContextAccessor();

            // register UAParser
            services.AddSingleton(Parser.GetDefault());


            return services;
        }


    }
}
