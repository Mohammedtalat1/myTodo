using Domain.IRepository.IBaseRepository;
using TODO.Domain.Entities;
using System.Threading.Tasks;

namespace TODO.Domain.IRepository
{
    /// <summary>
    /// ISP: extends IBaseRepository with role-specific queries.
    /// </summary>
    public interface IRolesRepository : IBaseRepository<Roles>
    {
        /// <summary>Find a role by its English name (e.g., "Developer").</summary>
        Task<Roles?> GetByNameAsync(string name);
    }
}
