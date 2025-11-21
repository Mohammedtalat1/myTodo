using TODO.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.Application.IService
{
    public interface IJwtService
    {
        string GenerateToken(Account account);
    }
}
