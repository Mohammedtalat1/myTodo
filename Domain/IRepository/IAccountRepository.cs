using TODO.Domain.Entites;
using Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.Domain.IRepository
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        Task<Account?> GetByEmailAsync(string email);

    }
}
