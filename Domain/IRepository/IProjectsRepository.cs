using Domain.IRepository.IBaseRepository;
using TODO.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TODO.Domain.IRepository
{
    public interface IProjectsRepository : IBaseRepository<Projects>
    {
        /// <summary>Get all projects the given user belongs to (via ProjectMember join).</summary>
        Task<IEnumerable<Projects>> GetByUserIdAsync(int userId);
    }
}
