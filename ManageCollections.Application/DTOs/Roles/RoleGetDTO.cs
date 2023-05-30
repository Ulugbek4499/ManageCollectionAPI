using ManageCollections.Application.DTOs.Permissions;

namespace ManageCollections.Application.DTOs.Roles
{
    public class RoleGetDTO : RoleBaseDTO
    {
        public Guid Id { get; set; }
        public ICollection<PermissionGetDTO>? Permissions { get; set; }
    }
}
