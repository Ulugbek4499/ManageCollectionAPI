namespace ManageCollections.Application.DTOs.Users
{
    public class UserCreateDTO : UserBaseDTO
    {
        public List<Guid>? RoleIds { get; set; }
    }
}
