namespace ManageCollections.Application.DTOs.Items
{
    public class ItemBaseDTO
    {
        public string? Name { get; set; }
        public string? Image { get; set; }
        public Guid CollectionId { get; set; }

        //  public List<CommentGetDTO> Comments { get; set; }
        //  public List<TagGetDTO>? Tags { get; set; }
    }
}
