using Application.IService.IBaseService;
using Application.Common;
using TODO.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TODO.Application.IService
{
    public interface IWorkItemsService : IBaseServices<WorkItemsDTO>
    {
        Task<Results<IEnumerable<WorkItemsDTO>>> GetByProjectAsync(int projectId);
        Task<Results<IEnumerable<WorkItemsDTO>>> GetByColumnAsync(int columnId);
        Task<Results<bool>> MoveToColumnAsync(int itemId, int columnId);
    }
}
