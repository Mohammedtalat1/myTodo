using Infrastructure.Repository.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TODO.Domain.Entities;
using TODO.Domain.IRepository;
using TODO.Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class ProjectMemberRepository : BaseRepository<ProjectMember>, IProjectMemberRepository
    {
        private readonly AppDbContext _context;

        public ProjectMemberRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(int userId, int projectId)
        {
            return await _context.ProjectMembers
                .AnyAsync(pm => pm.UserId == userId && pm.ProjectId == projectId);
        }
    }
}
