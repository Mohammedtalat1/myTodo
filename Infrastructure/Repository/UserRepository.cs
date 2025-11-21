using TODO.Domain.Entites;
using TODO.Domain.IRepository;
using TODO.Infrastructure.Data;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace TODO.Infrastructure.Repository
{
    
    public class UserRepository(AppDbContext productDbContext) : BaseRepository<Users>(productDbContext), IUserRepository
    {

    }
}
