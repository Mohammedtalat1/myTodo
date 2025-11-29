using Application.IService;
using Application.Service.Impl.BaseService;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TODO.Application.DTOs;
using TODO.Application.IService;
using TODO.Domain.Entities;
using TODO.Application.Entities;
using TODO.Domain.IRepository;

namespace TODO.Application.Service.Impl
{
    public class ProjectMemberService : BaseService<ProjectMember, ProjectMemberDTO>, IProjectMemberService
    {
        public ProjectMemberService(IProjectMemberRepository projectMemberRepository, IMapper mapper, ILogger<ProjectMemberService> logger)
            : base(projectMemberRepository, mapper, logger)
        {
        }

    }
}
