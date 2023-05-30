using System.Reflection;
using FluentValidation;
using ManageCollections.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace ManageCollections.Application
{
    public static class RegisterServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapProfiles));// Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
