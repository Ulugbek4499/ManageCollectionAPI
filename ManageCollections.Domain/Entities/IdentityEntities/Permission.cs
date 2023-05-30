namespace ManageCollections.Domain.Entities.IdentityEntities;

public class Permission : BaseAuditableEntity
{
    public string? PermissionName { get; set; }

    public virtual ICollection<Role>? Roles { get; set; }
}
