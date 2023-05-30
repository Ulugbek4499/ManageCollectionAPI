using ManageCollections.API.Services;
using ManageCollections.Application.Interfaces;

namespace ManageCollections.API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebUIServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();
            return services;
        }
    }
}
