using Infrastructure.Repository.BaseRepository;
using Microsoft.EntityFrameworkCore;
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

    }
}
