using Application.IService.IBaseService;
using Application.Common;
using TODO.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TODO.Application.IService
{
    public interface ICommentsService : IBaseServices<CommentsDTO>
    {
        Task<Results<IEnumerable<CommentsDTO>>> GetByWorkItemAsync(int workItemId);
    }
}
