using Infrastructure.Repository.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TODO.Domain.Entities;
using TODO.Domain.IRepository;
using TODO.Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class CommentsRepository : BaseRepository<Comments>, ICommentsRepository
    {
        private readonly AppDbContext _context;

        public CommentsRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
