using Domain.IRepository.IBaseRepository;
using TODO.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TODO.Domain.IRepository
{
    /// <summary>
    /// ISP: adds work-item-specific queries without bloating IBaseRepository.
    /// </summary>
    public interface IWorkItemsRepository : IBaseRepository<WorkItems>
    {
        Task<IEnumerable<WorkItems>> GetByProjectIdAsync(int projectId);
        Task<IEnumerable<WorkItems>> GetByColumnIdAsync(int columnId);
    }
}
