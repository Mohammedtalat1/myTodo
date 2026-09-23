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
    public class WorkItemsRepository : BaseRepository<WorkItems>, IWorkItemsRepository
    {
        private readonly AppDbContext _context;

        public WorkItemsRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkItems>> GetByProjectIdAsync(int projectId)
        {
            return await _context.WorkItems
                .Where(w => w.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<IEnumerable<WorkItems>> GetByColumnIdAsync(int columnId)
        {
            return await _context.WorkItems
                .Where(w => w.ColumnId == columnId)
                .ToListAsync();
        }
    }
}
