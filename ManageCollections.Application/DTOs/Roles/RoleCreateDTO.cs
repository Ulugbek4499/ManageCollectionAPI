namespace ManageCollections.Application.DTOs.Roles
{
    public class RoleCreateDTO : RoleBaseDTO
    {
        public List<Guid>? Permissions { get; set; }
    }
}
