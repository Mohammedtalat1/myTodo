using Application.Common;
using Application.Service.Impl.BaseService;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TODO.Application.DTOs;
using TODO.Application.IService;
using TODO.Domain.Entities;
using TODO.Domain.IRepository;

namespace TODO.Application.Service.Impl
{
    public class CommentsService : BaseService<Comments, CommentsDTO>, ICommentsService
    {
        private readonly ICommentsRepository _commentsRepo;

        public CommentsService(
            ICommentsRepository commentsRepository,
            IMapper mapper,
            ILogger<CommentsService> logger)
            : base(commentsRepository, mapper, logger)
        {
            _commentsRepo = commentsRepository;
        }

        public async Task<Results<IEnumerable<CommentsDTO>>> GetByWorkItemAsync(int workItemId)
        {
            try
            {
                var comments = await _commentsRepo.GetByWorkItemIdAsync(workItemId);
                return SuccessResult.Success(_mapper.Map<IEnumerable<CommentsDTO>>(comments));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching comments for work item {WorkItemId}", workItemId);
                return ErrorResult.Failed<IEnumerable<CommentsDTO>>(ex.Message);
            }
        }
    }
}
