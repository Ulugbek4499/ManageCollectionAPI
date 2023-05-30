namespace ManageCollections.Domain.Entities;

public class Comment : BaseAuditableEntity
{
    public string? Content { get; set; }

    public Guid ItemId { get; set; }
    public Item? Item { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }
}
