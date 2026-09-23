using Application.Common;
using Application.Service.Impl.BaseService;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TODO.Application.DTOs;
using TODO.Application.IService;
using TODO.Domain.Entities;
using TODO.Domain.IRepository;

namespace TODO.Application.Service.Impl
{
    /// <summary>
    /// Prevents duplicate memberships. OCP: overrides InsertAsync without touching BaseService.
    /// </summary>
    public class ProjectMemberService : BaseService<ProjectMember, ProjectMemberDTO>, IProjectMemberService
    {
        private readonly IProjectMemberRepository _memberRepo;

        public ProjectMemberService(
            IProjectMemberRepository projectMemberRepository,
            IMapper mapper,
            ILogger<ProjectMemberService> logger)
            : base(projectMemberRepository, mapper, logger)
        {
            _memberRepo = projectMemberRepository;
        }

        /// <summary>Rejects insert if the user is already a member of the project.</summary>
        public override async Task<Results<int>> InsertAsync(ProjectMemberDTO dto)
        {
            try
            {
                var isDuplicate = await _memberRepo.ExistsAsync(dto.UserId, dto.ProjectId);
                if (isDuplicate)
                {
                    _logger.LogWarning("Duplicate membership attempt: User {UserId} in Project {ProjectId}",
                        dto.UserId, dto.ProjectId);
                    return ErrorResult.Failed<int>("User is already a member of this project.");
                }

                return await base.InsertAsync(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding member {UserId} to project {ProjectId}",
                    dto.UserId, dto.ProjectId);
                return ErrorResult.Failed<int>(ex.Message);
            }
        }
    }
}
