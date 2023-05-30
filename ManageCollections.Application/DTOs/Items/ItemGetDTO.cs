using ManageCollections.Application.DTOs.Comments;
using ManageCollections.Application.DTOs.Tags;

namespace ManageCollections.Application.DTOs.Items
{
    public class ItemGetDTO : ItemBaseDTO
    {
        public Guid Id { get; set; }
        public ICollection<CommentGetDTO> Comments { get; set; }
        public ICollection<TagGetDTO> Tags { get; set; }
    }
}
