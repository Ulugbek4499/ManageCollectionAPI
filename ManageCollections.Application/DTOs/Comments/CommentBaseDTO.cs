namespace ManageCollections.Application.DTOs.Comments
{
    public class CommentBaseDTO
    {
        public string? Content { get; set; }
        public Guid ItemId { get; set; }
        public Guid UserId { get; set; }
    }
}
