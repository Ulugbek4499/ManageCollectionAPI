namespace ManageCollections.Application.DTOs.Roles
{
    public class RoleUpdateDTO : RoleBaseDTO
    {
        public Guid Id { get; set; }
        public List<Guid>? PermissionIds { get; set; }
    }
}
