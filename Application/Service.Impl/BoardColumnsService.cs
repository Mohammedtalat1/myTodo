using Application.Service.Impl.BaseService;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TODO.Application.DTOs;
using TODO.Application.IService;
using TODO.Domain.Entities;
using TODO.Domain.IRepository;

namespace TODO.Application.Service.Impl
{
    public class BoardColumnsService : BaseService<BoardColumns, BoardColumnsDTO>, IBoardColumnsService
    {
        public BoardColumnsService(
            IBoardColumnsRepository boardColumnsRepository,
            IMapper mapper,
            ILogger<BoardColumnsService> logger)
            : base(boardColumnsRepository, mapper, logger)
        {
        }
    }
}
