using Infrastructure.Repository.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TODO.Domain.Entities;
using TODO.Domain.IRepository;
using TODO.Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class RolesRepository : BaseRepository<Roles>, IRolesRepository
    {
        private readonly AppDbContext _context;

        public RolesRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Roles?> GetByNameAsync(string name)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Name_En != null && r.Name_En.ToLower() == name.ToLower());
        }
    }
}
