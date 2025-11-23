using AutoMapper;
using TODO.Application.DTOs;
using TODO.Domain.Entites;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Users, UserDTO>().ReverseMap();
        CreateMap<Roles, RolesDTO>().ReverseMap();
        CreateMap<Permissions, PermissionsDTO>().ReverseMap();
    }
}
