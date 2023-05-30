using ManageCollections.Domain.Enums;

namespace ManageCollections.Application.DTOs.Collections
{
    public class CollectionBaseDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public TopicType? Topic { get; set; }
        public string? Image { get; set; }

        public Guid UserId { get; set; }
    }
}
