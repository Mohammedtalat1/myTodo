using TODO.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IRepository.IBaseRepository;

namespace TODO.Domain.IRepository
{
    public interface IUserRepository : IBaseRepository<Users>
    {
        Task<Users?> GetByEmailAsync(string email);  

    }
}
