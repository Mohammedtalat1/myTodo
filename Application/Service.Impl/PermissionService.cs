using Application.IService;
using Application.Service.Impl.BaseService;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TODO.Application.DTOs;
using TODO.Application.IService;
using TODO.Domain.Entities;
using TODO.Domain.IRepository;

namespace TODO.Application.Service.Impl
{
    public class PermissionService : BaseService<Permissions, PermissionsDTO>, IPermissionsService
    {
        public PermissionService(IPermissionRepository permissionsRepository, IMapper mapper, ILogger<PermissionService> logger)
            : base(permissionsRepository, mapper, logger)
        {
        }

    }
}
