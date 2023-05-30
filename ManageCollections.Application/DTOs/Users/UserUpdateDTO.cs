namespace ManageCollections.Application.DTOs.Users
{
    public class UserUpdateDTO : UserBaseDTO
    {
        public Guid Id { get; set; }
        public List<Guid>? RoleIds { get; set; }
    }
}
