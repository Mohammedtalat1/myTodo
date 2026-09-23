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
    /// Boards service. OCP: on board creation, auto-creates 4 default Kanban columns.
    /// </summary>
    public class BoardsService : BaseService<Boards, BoardsDTO>, IBoardsService
    {
        private readonly IBoardColumnsRepository _columnsRepo;

        public BoardsService(
            IBoardsRepository boardsRepository,
            IBoardColumnsRepository columnsRepository,
            IMapper mapper,
            ILogger<BoardsService> logger)
            : base(boardsRepository, mapper, logger)
        {
            _columnsRepo = columnsRepository;
        }

        /// <summary>
        /// Creates a board and auto-populates it with default columns: To Do, In Progress, In Review, Done.
        /// </summary>
        public override async Task<Results<int>> InsertAsync(BoardsDTO dto)
        {
            try
            {
                var board = _mapper.Map<Boards>(dto);
                board.CreatedAt = DateTime.UtcNow;
                var result = await _repository.InsertAsync(board);

                if (result > 0)
                {
                    await CreateDefaultColumnsAsync(board.Id);
                }

                return SuccessResult.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating board for project {ProjectId}", dto.ProjectId);
                return ErrorResult.Failed<int>(ex.Message);
            }
        }

        // ── Private helpers ────────────────────────────────────────────────────────

        private async Task CreateDefaultColumnsAsync(int boardId)
        {
            var defaultColumns = new[]
            {
                new BoardColumns { Name = "To Do",      Order = 1, BoardId = boardId, CreatedAt = DateTime.UtcNow },
                new BoardColumns { Name = "In Progress", Order = 2, BoardId = boardId, CreatedAt = DateTime.UtcNow },
                new BoardColumns { Name = "In Review",  Order = 3, BoardId = boardId, CreatedAt = DateTime.UtcNow },
                new BoardColumns { Name = "Done",       Order = 4, BoardId = boardId, CreatedAt = DateTime.UtcNow }
            };

            foreach (var column in defaultColumns)
            {
                await _columnsRepo.InsertAsync(column);
            }

            _logger.LogInformation("Created 4 default columns for board {BoardId}", boardId);
        }
    }
}
