namespace ManageCollections.Application.DTOs.Tags
{
    public class TagUpdateDTO : TagBaseDTO
    {
        public Guid Id { get; set; }
        public List<Guid> ItemIds { get; set; }
    }
}
