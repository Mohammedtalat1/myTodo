using Infrastructure.Repository.BaseRepository;
using Microsoft.EntityFrameworkCore;
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

    }
}
