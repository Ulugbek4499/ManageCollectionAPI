using ManageCollections.Application.Abstractions;
using ManageCollections.Application.Interfaces;
using ManageCollections.Domain.Entities;

namespace Infrastructure.Services
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(IManageCollectionDbContext collectionDbContext) : base(collectionDbContext)
        {
        }
    }
}
