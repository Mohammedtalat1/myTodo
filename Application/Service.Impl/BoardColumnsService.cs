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
    public class BoardColumnsService : BaseService<BoardColumns, BoardColumnsDTO>, IBoardColumnsService
    {
        public BoardColumnsService(IBoardColumnsRepository boardRepository, IMapper mapper, ILogger<BoardColumnsService> logger)
            : base(boardRepository, mapper, logger)
        {
        }

    }
}
