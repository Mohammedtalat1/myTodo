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
    public class ProjectsRepository : BaseRepository<Projects>, IProjectsRepository
    {
        private readonly AppDbContext _context;

        public ProjectsRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Projects>> GetByUserIdAsync(int userId)
        {
            return await _context.ProjectMembers
                .Where(pm => pm.UserId == userId)
                .Include(pm => pm.Project)
                .Select(pm => pm.Project!)
                .ToListAsync();
        }
    }
}
