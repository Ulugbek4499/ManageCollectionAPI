using AutoMapper;
using ManageCollections.Application.DTOs.Collections;
using ManageCollections.Application.DTOs.Comments;
using ManageCollections.Application.DTOs.Items;
using ManageCollections.Application.DTOs.Permissions;
using ManageCollections.Application.DTOs.Roles;
using ManageCollections.Application.DTOs.Tags;
using ManageCollections.Application.DTOs.Users;
using ManageCollections.Domain.Entities;
using ManageCollections.Domain.Entities.IdentityEntities;

namespace ManageCollections.Application.Mappings
{
    public class MapProfiles : Profile
    {
        public MapProfiles()
        {
            CreateMap<PermissionCreateDTO, Permission>().ReverseMap();
            CreateMap<PermissionUpdateDTO, Permission>().ReverseMap();
            CreateMap<PermissionGetDTO, Permission>().ReverseMap();


            CreateMap<Role, RoleCreateDTO>().ReverseMap()
                  .ForMember(x => x.Users, t => t.Ignore())
                  .ForMember(x => x.Permissions, t => t.Ignore());

            CreateMap<Role, RoleUpdateDTO>().ReverseMap();

            CreateMap<Role, RoleGetDTO>().ReverseMap()
                .ForMember(x => x.Users, dto => dto.Ignore());


            CreateMap<User, UserCreateDTO>().ReverseMap()
                 .ForMember(x => x.Roles, t => t.Ignore())
                 .ForMember(x => x.Comments, t => t.Ignore())
                 .ForMember(x => x.Collections, t => t.Ignore());

            CreateMap<UserUpdateDTO, User>().ReverseMap();

            CreateMap<UserGetDTO, User>().ReverseMap();


            CreateMap<Collection, CollectionCreateDTO>().ReverseMap()
                  .ForMember(x => x.Items, t => t.Ignore());
            CreateMap<CollectionUpdateDTO, Collection>().ReverseMap();
            CreateMap<CollectionGetDTO, Collection>().ReverseMap();


            CreateMap<Item, ItemCreateDTO>().ReverseMap()
                .ForMember(x => x.Tags, t => t.Ignore())
                .ForMember(x => x.Comments, t => t.Ignore());

            CreateMap<ItemUpdateDTO, Item>().ReverseMap();
            CreateMap<Item, ItemGetDTO>().ReverseMap();


            CreateMap<CommentCreateDTO, Comment>().ReverseMap();
            CreateMap<CommentUpdateDTO, Comment>().ReverseMap();
            CreateMap<CommentGetDTO, Comment>().ReverseMap();



            CreateMap<Tag, TagCreateDTO>().ReverseMap()
                .ForMember(x => x.Items, t => t.Ignore());
            CreateMap<TagUpdateDTO, Tag>().ReverseMap();
            CreateMap<Tag, TagGetDTO>().ReverseMap();
        }
    }
}
