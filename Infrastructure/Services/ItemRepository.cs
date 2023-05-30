using ManageCollections.Application.Abstractions;
using ManageCollections.Application.Interfaces;
using ManageCollections.Domain.Entities;

namespace Infrastructure.Services
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(IManageCollectionDbContext collectionDbContext) : base(collectionDbContext)
        {
        }
    }
}
