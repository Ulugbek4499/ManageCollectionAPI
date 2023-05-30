using Infrastructure.DataAccess.Interceptor;
using ManageCollections.Application.Abstractions;
using ManageCollections.Domain.Entities;
using ManageCollections.Domain.Entities.IdentityEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess
{
    public class ManageCollectionDbContext : DbContext, IManageCollectionDbContext
    {
        private readonly AuditableEntitySaveChangesInterceptor _interceptor;

        public ManageCollectionDbContext(DbContextOptions<ManageCollectionDbContext> options,
                AuditableEntitySaveChangesInterceptor interceptor)
                : base(options)
        {
            _interceptor = interceptor;
        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_interceptor);
        }
    }

}
