using Domain.IRepository.IBaseRepository;
using TODO.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TODO.Domain.IRepository
{
    public interface IAttachmentsRepository : IBaseRepository<Attachments>
    {
        Task<IEnumerable<Attachments>> GetByWorkItemIdAsync(int workItemId);
    }
}
