using Infrastructure.Repository.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TODO.Domain.Entities;
using TODO.Domain.IRepository;
using TODO.Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class BoardColumnsRepository : BaseRepository<BoardColumns>, IBoardColumnsRepository
    {
        private readonly AppDbContext _context;

        public BoardColumnsRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }


    }
}
