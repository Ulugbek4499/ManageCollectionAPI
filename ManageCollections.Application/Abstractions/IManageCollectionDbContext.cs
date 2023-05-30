using ManageCollections.Domain.Entities;
using ManageCollections.Domain.Entities.IdentityEntities;
using Microsoft.EntityFrameworkCore;

namespace ManageCollections.Application.Abstractions
{
    public interface IManageCollectionDbContext
    {
        DbSet<T> Set<T>() where T : class;
        DbSet<Permission> Permissions { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Collection> Collections { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<Tag> Tags { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
