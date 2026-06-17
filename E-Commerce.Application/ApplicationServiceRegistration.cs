using E_Commerce.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            // Add MediatR services to DI container
            services.AddMediatR(typeof(ApplicationServiceRegistration));
            // Add FluentValidation services to DI container
            services.AddValidatorsFromAssembly(typeof(ApplicationServiceRegistration).Assembly); // it will automatically scan the assembly for any classes that implement IValidator<T> and register them with the DI container.

            // register the ValidationBehavior as a pipeline behavior in MediatR. // this makes validations automatically run 
            services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
