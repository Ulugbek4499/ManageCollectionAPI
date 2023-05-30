using ManageCollections.Application.DTOs.Roles;

namespace ManageCollections.Application.DTOs.Users
{
    public class UserGetDTO : UserBaseDTO
    {
        public Guid Id { get; set; }
        public ICollection<RoleGetDTO> Roles { get; set; }
    }
}
