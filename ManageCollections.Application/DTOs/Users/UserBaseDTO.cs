namespace ManageCollections.Application.DTOs.Users
{
    public class UserBaseDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

        //public List<CollectionGetDTO>? Collections { get; set; }
        //public List<CommentGetDTO>? Comments { get; set; }
    }
}
