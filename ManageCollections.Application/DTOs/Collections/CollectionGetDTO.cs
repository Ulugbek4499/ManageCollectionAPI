using ManageCollections.Application.DTOs.Items;

namespace ManageCollections.Application.DTOs.Collections
{
    public class CollectionGetDTO : CollectionBaseDTO
    {
        public Guid Id { get; set; }
        public ICollection<ItemGetDTO> Items { get; set; }
    }
}
