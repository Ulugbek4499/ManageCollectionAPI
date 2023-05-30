using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Interceptor;
using Infrastructure.Services;
using ManageCollections.Application.Abstractions;
using ManageCollections.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class RegesterService
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IManageCollectionDbContext, ManageCollectionDbContext>(x => x.UseNpgsql(configuration.GetConnectionString("PostgresDB")));
            services.AddTransient<AuditableEntitySaveChangesInterceptor>();
            services.AddScoped<ICollectionRepository, CollectionRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }

}
