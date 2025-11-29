using Application.IService;
using Application.Service.Impl.BaseService;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TODO.Application.DTOs;
using TODO.Application.IService;
using TODO.Domain.Entities;
using TODO.Application.Entities;
using TODO.Domain.IRepository;

namespace TODO.Application.Service.Impl
{
    public class WorkItemsService : BaseService<WorkItems, WorkItemsDTO>, IWorkItemsService
    {
        public WorkItemsService(IWorkItemsRepository workItemsRepository, IMapper mapper, ILogger<WorkItemsService> logger)
            : base(workItemsRepository, mapper, logger)
        {
        }

    }
}
