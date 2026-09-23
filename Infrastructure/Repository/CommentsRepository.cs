using Infrastructure.Repository.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Comments>> GetByWorkItemIdAsync(int workItemId)
        {
            return await _context.Comments
                .Where(c => c.TaskId == workItemId)
                .ToListAsync();
        }
    }
}
