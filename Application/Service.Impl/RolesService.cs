using Application.IService;
using Application.Service.Impl.BaseService;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TODO.Application.DTOs;
using TODO.Application.IService;
using TODO.Domain.Entites;
using TODO.Domain.IRepository;

namespace TODO.Application.Service.Impl
{
    public class RolesService : BaseService<Roles, RolesDTO>, IRolesService
    {
        public RolesService(IRolesRepository rolesRepository, IMapper mapper, ILogger<RolesService> logger)
            : base(rolesRepository, mapper, logger)
        {
        }

    }
}
