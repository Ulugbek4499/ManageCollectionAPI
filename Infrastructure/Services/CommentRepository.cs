using ManageCollections.Application.Abstractions;
using ManageCollections.Application.Interfaces;
using ManageCollections.Domain.Entities;

namespace Infrastructure.Services
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(IManageCollectionDbContext collectionDbContext) : base(collectionDbContext)
        {
        }
    }
}
