using ManageCollections.Application.DTOs.Items;

namespace ManageCollections.Application.DTOs.Tags
{
    public class TagGetDTO : TagBaseDTO
    {
        public Guid Id { get; set; }
        ICollection<ItemGetDTO> Items { get; set; }

    }
}
