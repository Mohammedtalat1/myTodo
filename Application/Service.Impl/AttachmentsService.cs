using Application.IService;
using Application.Service.Impl.BaseService;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TODO.Application.DTOs;
using TODO.Application.IService;
using TODO.Application.Entities;
using TODO.Domain.IRepository;

namespace TODO.Application.Service.Impl
{
    public class AttachmentsService : BaseService<Attachments, AttachmentsDTO>, IAttachmentsService
    {
        public AttachmentsService(IAttachmentsRepository attachmentRepository, IMapper mapper, ILogger<AttachmentsService> logger)
            : base(attachmentRepository, mapper, logger)
        {
        }

    }
}
