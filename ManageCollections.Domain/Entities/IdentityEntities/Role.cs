namespace ManageCollections.Domain.Entities.IdentityEntities;

public class Role : BaseAuditableEntity
{
    public string? Name { get; set; }

    public virtual ICollection<Permission>? Permissions { get; set; }
    public virtual ICollection<User>? Users { get; set; }
}
