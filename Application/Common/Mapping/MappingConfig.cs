using Application.Models;
using AutoMapper;
using TODO.Application.DTOs;
using TODO.Domain.Entites;


namespace Application.Common
{
    public class MappingConfig
    {
        public static IMapper Configure()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Account, AccountDTO>().ReverseMap();
                cfg.CreateMap<Users, UserDTO>().ReverseMap();
                cfg.CreateMap<Roles, RolesDTO>().ReverseMap();


            }).CreateMapper(); 

        }
    }
}
