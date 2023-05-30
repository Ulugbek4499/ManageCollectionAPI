namespace ManageCollections.Domain.Entities;

public class Item : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? Image { get; set; }

    public Guid CollectionId { get; set; }
    public Collection? Collection { get; set; }

    public virtual ICollection<Comment>? Comments { get; set; }
    public virtual ICollection<Tag>? Tags { get; set; }
}
