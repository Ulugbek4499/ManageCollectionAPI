namespace ManageCollections.Domain.Entities;

public class Collection : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public TopicType? Topic { get; set; }
    public string? Image { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }

    public ICollection<Item>? Items { get; set; }
}
