namespace ManageCollections.Application.DTOs.Collections
{
    public class CollectionUpdateDTO : CollectionBaseDTO
    {
        public Guid Id { get; set; }
        public List<Guid>? ItemIds { get; set; }
    }
}
