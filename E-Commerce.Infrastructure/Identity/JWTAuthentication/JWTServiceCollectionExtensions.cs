using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_Commerce.Infrastructure.Identity.JWT;

public static class JwtServiceCollectionExtensions
{
    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // to use options pattern and use jwtSettings class  not using configuraiton direclty
        services.Configure<JWTSettings>(
     configuration.GetSection("JWTSettings"));
        var jwtSettings = configuration
           .GetSection("JWTSettings")
           .Get<JWTSettings>()
           ?? throw new InvalidOperationException(
               "JWTSettings section is missing.");



        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {


                // jwt configuration
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret!)),

                    ClockSkew = TimeSpan.Zero

                };
            });

        return services;
    }
}