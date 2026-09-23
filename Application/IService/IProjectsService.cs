using Application.IService.IBaseService;
using Application.Common;
using TODO.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TODO.Application.IService
{
    /// <summary>
    /// ISP: inherits base CRUD, adds project-specific operations.
    /// InsertAsync signature extended to accept creatorId for auto-membership.
    /// </summary>
    public interface IProjectsService : IBaseServices<ProjectsDTO>
    {
        /// <summary>Create a project and auto-enroll creatorId as Owner.</summary>
        Task<Results<int>> InsertAsync(ProjectsDTO dto, int creatorId);

        /// <summary>Get all projects the given user is a member of.</summary>
        Task<Results<IEnumerable<ProjectsDTO>>> GetUserProjectsAsync(int userId);
    }
}
