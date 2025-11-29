using AutoMapper;
using TODO.Application.DTOs;
using TODO.Application.Entities;
using TODO.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Users, UserDTO>().ReverseMap();
        CreateMap<Roles, RolesDTO>().ReverseMap();
        CreateMap<Permissions, PermissionsDTO>().ReverseMap();
        CreateMap<Attachments, AttachmentsDTO>().ReverseMap();
        CreateMap<BoardColumns, BoardColumnsDTO>().ReverseMap();
        CreateMap<Boards, BoardsDTO>().ReverseMap();
        CreateMap<Comments, CommentsDTO>().ReverseMap();
        CreateMap<ProjectMember, ProjectMemberDTO>().ReverseMap();
        CreateMap<Projects, ProjectsDTO>().ReverseMap();
        CreateMap<WorkItems, WorkItemsDTO>().ReverseMap();
    }
}
