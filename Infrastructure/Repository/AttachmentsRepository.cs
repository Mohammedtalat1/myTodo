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
    public class AttachmentsRepository : BaseRepository<Attachments>, IAttachmentsRepository
    {
        private readonly AppDbContext _context;

        public AttachmentsRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Attachments>> GetByWorkItemIdAsync(int workItemId)
        {
            return await _context.Attachments
                .Where(a => a.WorkItemId == workItemId)
                .ToListAsync();
        }
    }
}
