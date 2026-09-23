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
    public class AttachmentsService : BaseService<Attachments, AttachmentsDTO>, IAttachmentsService
    {
        private readonly IAttachmentsRepository _attachmentsRepo;

        public AttachmentsService(
            IAttachmentsRepository attachmentsRepository,
            IMapper mapper,
            ILogger<AttachmentsService> logger)
            : base(attachmentsRepository, mapper, logger)
        {
            _attachmentsRepo = attachmentsRepository;
        }

        public async Task<Results<IEnumerable<AttachmentsDTO>>> GetByWorkItemAsync(int workItemId)
        {
            try
            {
                var attachments = await _attachmentsRepo.GetByWorkItemIdAsync(workItemId);
                return SuccessResult.Success(_mapper.Map<IEnumerable<AttachmentsDTO>>(attachments));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching attachments for work item {WorkItemId}", workItemId);
                return ErrorResult.Failed<IEnumerable<AttachmentsDTO>>(ex.Message);
            }
        }
    }
}
