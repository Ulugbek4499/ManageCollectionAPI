using ManageCollections.Application.Abstractions;
using ManageCollections.Application.Interfaces;
using ManageCollections.Domain.Entities.IdentityEntities;

namespace Infrastructure.Services
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        
        public PermissionRepository(IManageCollectionDbContext collectionDbContext) : base(collectionDbContext)
        {
            Console.WriteLine("Permission repository is working");
        }
    }
}
