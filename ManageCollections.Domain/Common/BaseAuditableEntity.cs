
namespace ManageCollections.Domain.Common;
public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime CreateDate { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}
