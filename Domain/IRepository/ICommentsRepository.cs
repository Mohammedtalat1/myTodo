using Domain.IRepository.IBaseRepository;
using TODO.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TODO.Domain.IRepository
{
    public interface ICommentsRepository : IBaseRepository<Comments>
    {
        Task<IEnumerable<Comments>> GetByWorkItemIdAsync(int workItemId);
    }
}
