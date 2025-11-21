using TODO.Domain.Entites;
using TODO.Domain.IRepository;
using TODO.Infrastructure.Data;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.Infrastructure.Repository
{
   
    public class RolesRepository(AppDbContext productDbContext) : BaseRepository<Roles>(productDbContext), IRolesRepository
    {

    }
}
