using AutoMapper;
using TODO.Application.DTOs;
using TODO.Domain.Entities;

namespace Application.Common.Mapping
{
    /// <summary>
    /// AutoMapper profile — maps all domain entities to DTOs bidirectionally.
    /// New mappings must be added here (OCP: extend, do not scatter mappings across classes).
    /// </summary>
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

            // Register flow: map RegisterDTO → Users entity
            CreateMap<RegisterDTO, Users>()
                .ForMember(dest => dest.Password, opt => opt.Ignore()); // hashed in service
        }
    }
}
