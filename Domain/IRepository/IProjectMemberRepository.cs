using Domain.IRepository.IBaseRepository;
using TODO.Domain.Entities;
using System.Threading.Tasks;

namespace TODO.Domain.IRepository
{
    public interface IProjectMemberRepository : IBaseRepository<ProjectMember>
    {
        /// <summary>Returns true if the user is already a member of the project.</summary>
        Task<bool> ExistsAsync(int userId, int projectId);
    }
}
