namespace ManageCollections.Application.DTOs.Tags
{
    public class TagCreateDTO : TagBaseDTO
    {
        public List<Guid> ItemIds { get; set; }
    }
}
