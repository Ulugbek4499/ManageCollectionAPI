using ManageCollections.Application.Abstractions;
using ManageCollections.Application.Interfaces;
using ManageCollections.Domain.Entities.IdentityEntities;

namespace Infrastructure.Services
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(IManageCollectionDbContext collectionDb) : base(collectionDb)
        {
        }
    }
}
