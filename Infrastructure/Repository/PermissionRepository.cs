using TODO.Domain.Entities;
using TODO.Domain.IRepository;
using TODO.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Repository.BaseRepository;

namespace TODO.Infrastructure.Repository
{
    public class PermissionRepository(AppDbContext productDbContext) : BaseRepository<Permissions>(productDbContext), IPermissionRepository
    {
    }
}
