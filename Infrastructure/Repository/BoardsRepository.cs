using Infrastructure.Repository.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TODO.Domain.Entities;
using TODO.Domain.IRepository;
using TODO.Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class BoardsRepository : BaseRepository<Boards>, IBoardsRepository
    {
        private readonly AppDbContext _context;

        public BoardsRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
