
namespace ManageCollections.Domain.Entities;

public class Tag : BaseAuditableEntity
{
    public string? Name { get; set; }

    public virtual ICollection<Item>? Items { get; set; }
}
