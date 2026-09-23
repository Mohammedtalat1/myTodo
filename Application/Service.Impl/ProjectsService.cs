using Application.Common;
using Application.Service.Impl.BaseService;
using AutoMapper;
using Domain.Common.enums;
using Microsoft.Extensions.Logging;
using TODO.Application.DTOs;
using TODO.Application.IService;
using TODO.Domain.Entities;
using TODO.Domain.IRepository;

namespace TODO.Application.Service.Impl
{
    /// <summary>
    /// Manages projects. OCP: extends BaseService for CRUD, adds project-specific behavior.
    /// On create: auto-enrolls the creator as Owner in ProjectMember.
    /// </summary>
    public class ProjectsService : BaseService<Projects, ProjectsDTO>, IProjectsService
    {
        private readonly IProjectsRepository _projectsRepo;
        private readonly IProjectMemberRepository _memberRepo;

        public ProjectsService(
            IProjectsRepository projectsRepository,
            IProjectMemberRepository memberRepository,
            IMapper mapper,
            ILogger<ProjectsService> logger)
            : base(projectsRepository, mapper, logger)
        {
            _projectsRepo = projectsRepository;
            _memberRepo = memberRepository;
        }

        /// <summary>
        /// Creates a project AND auto-adds the creator as Owner.
        /// Overloaded with creatorId — base InsertAsync still works for admin flows.
        /// </summary>
        public async Task<Results<int>> InsertAsync(ProjectsDTO dto, int creatorId)
        {
            try
            {
                var entity = _mapper.Map<Projects>(dto);
                entity.CreatedAt = DateTime.UtcNow;

                var result = await _projectsRepo.InsertAsync(entity);

                if (result > 0 && creatorId > 0)
                {
                    await AddOwnerMembershipAsync(entity.Id, creatorId);
                }

                return SuccessResult.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating project with owner {CreatorId}", creatorId);
                return ErrorResult.Failed<int>(ex.Message);
            }
        }

        /// <summary>Get all projects the user belongs to.</summary>
        public async Task<Results<IEnumerable<ProjectsDTO>>> GetUserProjectsAsync(int userId)
        {
            try
            {
                var projects = await _projectsRepo.GetByUserIdAsync(userId);
                var dtos = _mapper.Map<IEnumerable<ProjectsDTO>>(projects);
                return SuccessResult.Success(dtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching projects for user {UserId}", userId);
                return ErrorResult.Failed<IEnumerable<ProjectsDTO>>(ex.Message);
            }
        }

        // ── Private helpers ────────────────────────────────────────────────────────

        private async Task AddOwnerMembershipAsync(int projectId, int userId)
        {
            var alreadyMember = await _memberRepo.ExistsAsync(userId, projectId);
            if (alreadyMember) return;

            var membership = new ProjectMember
            {
                UserId = userId,
                ProjectId = projectId,
                RoleInProject = RoleInProjectEnum.Owner,
                CreatedAt = DateTime.UtcNow
            };

            await _memberRepo.InsertAsync(membership);
        }
    }
}
