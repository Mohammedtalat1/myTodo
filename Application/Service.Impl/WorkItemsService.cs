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
    /// <summary>
    /// Work item service. OCP: extends BaseService with domain-specific Kanban operations.
    /// </summary>
    public class WorkItemsService : BaseService<WorkItems, WorkItemsDTO>, IWorkItemsService
    {
        private readonly IWorkItemsRepository _workItemsRepo;

        public WorkItemsService(
            IWorkItemsRepository workItemsRepository,
            IMapper mapper,
            ILogger<WorkItemsService> logger)
            : base(workItemsRepository, mapper, logger)
        {
            _workItemsRepo = workItemsRepository;
        }

        public async Task<Results<IEnumerable<WorkItemsDTO>>> GetByProjectAsync(int projectId)
        {
            try
            {
                var items = await _workItemsRepo.GetByProjectIdAsync(projectId);
                return SuccessResult.Success(_mapper.Map<IEnumerable<WorkItemsDTO>>(items));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching work items for project {ProjectId}", projectId);
                return ErrorResult.Failed<IEnumerable<WorkItemsDTO>>(ex.Message);
            }
        }

        public async Task<Results<IEnumerable<WorkItemsDTO>>> GetByColumnAsync(int columnId)
        {
            try
            {
                var items = await _workItemsRepo.GetByColumnIdAsync(columnId);
                return SuccessResult.Success(_mapper.Map<IEnumerable<WorkItemsDTO>>(items));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching work items for column {ColumnId}", columnId);
                return ErrorResult.Failed<IEnumerable<WorkItemsDTO>>(ex.Message);
            }
        }

        /// <summary>Move a work item to a different column (Kanban drag-and-drop).</summary>
        public async Task<Results<bool>> MoveToColumnAsync(int itemId, int columnId)
        {
            try
            {
                var item = await _workItemsRepo.GetByIdAsync(itemId);
                if (item == null)
                    return ErrorResult.Failed<bool>($"WorkItem with Id {itemId} not found.");

                item.ColumnId = columnId;
                item.UpdatedAt = DateTime.UtcNow;
                await _workItemsRepo.UpdateAsync(item);

                _logger.LogInformation("WorkItem {ItemId} moved to column {ColumnId}", itemId, columnId);
                return SuccessResult.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error moving work item {ItemId} to column {ColumnId}", itemId, columnId);
                return ErrorResult.Failed<bool>(ex.Message);
            }
        }
    }
}
