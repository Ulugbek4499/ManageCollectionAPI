using ManageCollections.Domain.Entities.IdentityEntities;

namespace ManageCollections.Domain.Entities;

public class User : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }

    public virtual ICollection<Role>? Roles { get; set; }
    public virtual ICollection<Collection>? Collections { get; set; }
    public virtual ICollection<Comment>? Comments { get; set; }
}
