namespace ManageCollections.Application.DTOs.Items
{
    public class ItemUpdateDTO : ItemBaseDTO
    {
        public Guid Id { get; set; }
        public List<Guid>? CommentIds { get; set; }
        public List<Guid>? TagIds { get; set; }
    }
}
