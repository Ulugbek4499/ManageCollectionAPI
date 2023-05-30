using ManageCollections.Application.Abstractions;
using ManageCollections.Application.Interfaces;
using ManageCollections.Domain.Entities;

namespace Infrastructure.Services
{
    public class CollectionRepository : Repository<Collection>, ICollectionRepository
    {
        public CollectionRepository(IManageCollectionDbContext collectionDbContext) : base(collectionDbContext)
        {
        }
    }
}
